using Autofac;
using BuildingBlocks.Infrastructure.EventBus.Contracts;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BuildingBlocks.Infrastructure.EventBus
{
    public class EventBus : IEventBus
    {
        private readonly IServiceBusPersisterConnection _serviceBusPersisterConnection;
        private readonly IEventBusSubcriptionManager _eventBusSubcriptionManager;
        private const string INTEGRATION_EVENT_SUFFIX = "IntegrationEvent";
        private readonly string AUTOFAC_SCOPE_NAME = "eshop_event_bus";
        private readonly SubscriptionClient _subscriptionClient;
        private readonly ILogger<EventBus> _logger;
        private readonly ILifetimeScope _autofac;

        public EventBus(
            IServiceBusPersisterConnection serviceBusPersisterConnection,
            IEventBusSubcriptionManager eventBusSubcriptionManager,
            string subscriptionClientName,
            ILogger<EventBus> logger,
            ILifetimeScope autofac)
        {
            this._serviceBusPersisterConnection = serviceBusPersisterConnection;
            this._eventBusSubcriptionManager = eventBusSubcriptionManager;
            this._subscriptionClient = new SubscriptionClient(serviceBusPersisterConnection.ServiceBusConnectionStringBuilder, subscriptionClientName);
            this._autofac = autofac;
            this._logger = logger;

            RemoveDefaultRule();
            RegisterSubscriptionClientMessageHandler();
        }

        private void RemoveDefaultRule()
        {
            try
            {
                this._subscriptionClient.RemoveRuleAsync(RuleDescription.DefaultRuleName).GetAwaiter().GetResult();
            }
            catch (MessagingEntityNotFoundException)
            {
                this._logger.LogWarning("The messaging entity {DefaultRuleName} Could not be found.", RuleDescription.DefaultRuleName);
            }
        }

        public async Task Publish(IntegrationEvent integrationEvent)
        {
            var integrationEventName = IntegrationEventName(integrationEvent);
            var json = JsonConvert.SerializeObject(integrationEvent);
            var body = Encoding.UTF8.GetBytes(json);

            var message = new Message
            {
                MessageId = Guid.NewGuid().ToString(),
                Body = body,
                Label = integrationEventName
            };

            var topicClient = this._serviceBusPersisterConnection.CreateModel();

            await topicClient.SendAsync(message);
        }

        public async Task Subscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var integrationEventName = IntegrationEventName<T>();

            var containsKey = this._eventBusSubcriptionManager.HasSubscriptionsForEvent<T>();

            if (!containsKey)
            {
                try
                {
                    await this._subscriptionClient.AddRuleAsync(new RuleDescription
                    {
                        Filter = new CorrelationFilter
                        { 
                            Label = integrationEventName
                        }
                    });
                }
                catch (ServiceBusException)
                {
                    this._logger.LogWarning("The messaging entity {eventName} already exists.", integrationEventName);
                }
            }

            this._logger.LogInformation("Subscribing to event {EventName} with {EventHandler}", integrationEventName, typeof(TH).Name);
        }

        public void SubscribeDynamic<TH>(string eventName) where TH : IDynamicIntegrationEventHandler
        {
            this._logger.LogInformation("Subscribing to dynamic event {EventName} with {EventHandler}", eventName, typeof(TH).Name);

            this._eventBusSubcriptionManager.AddDynamicSubcription<TH>(eventName);
        }

        public async Task Unsubscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var integrationEventName = IntegrationEventName<T>();

            try
            {
                await this._subscriptionClient.RemoveRuleAsync(integrationEventName);
            }
            catch (MessagingEntityNotFoundException)
            {
                this._logger.LogWarning("The messaging entity {eventName} Could not be found.", integrationEventName);
            }

            this._logger.LogInformation("Unsubscribing from event {EventName}", integrationEventName);

            this._eventBusSubcriptionManager.RemoveSubscription<T, TH>();
        }

        public void UnsubscribeDynamic<TH>(string eventName) where TH : IDynamicIntegrationEventHandler
        {
            this._logger.LogInformation("Unsubscribing from dynamic event {EventName}", eventName);

            this._eventBusSubcriptionManager.RemoveDynamicSubscription<TH>(eventName);
        }

        public void Dispose()
        {
            this._eventBusSubcriptionManager.Clear();
        }

        private string IntegrationEventName(IntegrationEvent integrationEvent) 
            => integrationEvent.GetType().Name.Replace(INTEGRATION_EVENT_SUFFIX, "");

        private string IntegrationEventName<T>() where T : IntegrationEvent
            => typeof(T).Name.Replace(INTEGRATION_EVENT_SUFFIX, "");

        private void RegisterSubscriptionClientMessageHandler()
        {
            this._subscriptionClient.RegisterMessageHandler(async (Message message, CancellationToken token) =>
            {
                var eventName = $"{message.Label}{INTEGRATION_EVENT_SUFFIX}";
                var messageData = Encoding.UTF8.GetString(message.Body);

                if (await ProcessEvent(eventName, messageData))
                {
                    await this._subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);
                }
            },
            new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 10,
                AutoComplete = false
            });
        }
        private async Task<bool> ProcessEvent(string eventName, string message)
        {
            var processed = false;

            if (this._eventBusSubcriptionManager.HasSubscriptionsForEvent(eventName))
            {
                using (var scope = this._autofac.BeginLifetimeScope(AUTOFAC_SCOPE_NAME))
                {
                    var subscriptions = this._eventBusSubcriptionManager.GetHandlersForEvent(eventName);

                    foreach (var subscription in subscriptions)
                    {
                        if (subscription.IsDynamic)
                        {
                            var handler = scope.ResolveOptional(subscription.HandlerType) as IDynamicIntegrationEventHandler;
                            if (handler == null) continue;

                            dynamic eventData = JObject.Parse(message);

                            await handler.Hande(eventData);
                        }
                        else
                        {
                            var handler = scope.ResolveOptional(subscription.HandlerType);
                            if (handler == null) continue;

                            var eventType = this._eventBusSubcriptionManager.GetEventTypeByName(eventName);

                            var integrationEvent = JsonConvert.DeserializeObject(message, eventType);
                            var concreteType = typeof(IIntegrationEventHandler<>).MakeGenericType(eventType);

                            await (Task)concreteType.GetMethod("Handle").Invoke(handler, new object[] { integrationEvent });
                        }
                    }
                }
                processed = true;
            }

            return processed;
        }
        private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            var ex = exceptionReceivedEventArgs.Exception;
            var context = exceptionReceivedEventArgs.ExceptionReceivedContext;

            this._logger.LogError(ex, "ERROR handling message: {ExceptionMessage} - Context: {@ExceptionContext}", ex.Message, context);

            return Task.CompletedTask;
        }
    }
}

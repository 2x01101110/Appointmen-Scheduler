using BuildingBlocks.Infrastructure.EventBus.Contracts;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BuildingBlocks.Infrastructure.EventBus
{
    public class InMemoryEventBusSubcriptionManager : IEventBusSubcriptionManager
    {
        private readonly Dictionary<string, List<SubscriptionInfo>> _handlers;
        private readonly List<Type> _eventTypes;

        public bool IsEmpty => !this._handlers.Keys.Any();

        public event EventHandler<string> OnEventRemoved;

        public InMemoryEventBusSubcriptionManager()
        {
            this._handlers = new Dictionary<string, List<SubscriptionInfo>>();
            this._eventTypes = new List<Type>();
        }

        public void AddSubscription<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var eventName = GetEventKey<T>();

            DoAddSubscription(typeof(TH), eventName, isDynamic: false);

            if (!this._eventTypes.Contains(typeof(T)))
            {
                this._eventTypes.Add(typeof(T));
            }
        }
        public void AddDynamicSubcription<TH>(string eventName) where TH : IDynamicIntegrationEventHandler
        {
            DoAddSubscription(typeof(TH), eventName, isDynamic: true);
        }

        public void RemoveSubscription<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var handlerToRemove = FindSubscriptionToRemove<T, TH>();
            var eventName = GetEventKey<T>();

            DoRemoveHandler(eventName, handlerToRemove);
        }
        public void RemoveDynamicSubscription<TH>(string eventName) where TH : IDynamicIntegrationEventHandler
        {
            var handlerToRemove = FindDynamicSubscriptionToRemove<TH>(eventName);
            DoRemoveHandler(eventName, handlerToRemove);
        }

        public bool HasSubscriptionsForEvent(string eventName) => this._handlers.ContainsKey(eventName);
        public bool HasSubscriptionsForEvent<T>() where T : IntegrationEvent
        {
            var key = GetEventKey<T>();
            return HasSubscriptionsForEvent(key);
        }

        public Type GetEventTypeByName(string eventName) => this._eventTypes.SingleOrDefault(x => x.Name == eventName);

        public IEnumerable<SubscriptionInfo> GetHandlersForEvent<T>() where T : IntegrationEvent
        {
            var key = GetEventKey<T>();
            return GetHandlersForEvent(key);
        }
        public IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName) => this._handlers[eventName];

        public string GetEventKey<T>() => typeof(T).Name;

        public void Clear() => this._handlers.Clear();
        
        private void DoAddSubscription(Type handlerType, string eventName, bool isDynamic)
        {
            if (!HasSubscriptionsForEvent(eventName))
            {
                this._handlers.Add(eventName, new List<SubscriptionInfo>());
            }

            if (this._handlers[eventName].Any(x => x.HandlerType == handlerType))
            {
                throw new ArgumentException($"Handler Type {handlerType.Name} already registered for '{eventName}'", nameof(handlerType));
            }

            if (isDynamic)
            {
                this._handlers[eventName].Add(SubscriptionInfo.Dynamic(handlerType));
            }
            else
            {
                this._handlers[eventName].Add(SubscriptionInfo.Typed(handlerType));
            }
        }
        private SubscriptionInfo FindSubscriptionToRemove<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>
        {
            var eventName = GetEventKey<T>();

            return DoFindSubscriptionToRemove(eventName, typeof(TH));
        }
        private SubscriptionInfo DoFindSubscriptionToRemove(string eventName, Type handlerType)
        {
            if (!HasSubscriptionsForEvent(eventName)) return null;

            return this._handlers[eventName].SingleOrDefault(x => x.HandlerType == handlerType);
        }
        private void DoRemoveHandler(string eventName, SubscriptionInfo subscriptionInfo)
        {
            if (subscriptionInfo != null)
            {
                this._handlers[eventName].Remove(subscriptionInfo);

                if (!this._handlers[eventName].Any())
                {
                    this._handlers.Remove(eventName);

                    var eventType = this._eventTypes.SingleOrDefault(x => x.Name == eventName);

                    if (eventType != null)
                    {
                        this._eventTypes.Remove(eventType);
                    }

                    RaiseOnEventRemoved(eventName);
                }
            }
        }
        private void RaiseOnEventRemoved(string eventName)
        {
            var handler = OnEventRemoved;
            handler?.Invoke(this, eventName);
        }
        private SubscriptionInfo FindDynamicSubscriptionToRemove<TH>(string eventName)
        {
            return DoFindSubscriptionToRemove(eventName, typeof(TH));
        }
    }
}

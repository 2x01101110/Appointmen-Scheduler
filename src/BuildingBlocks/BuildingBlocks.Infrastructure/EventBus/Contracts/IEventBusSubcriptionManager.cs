using BuildingBlocks.Infrastructure.EventBus.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingBlocks.Infrastructure.EventBus
{
    public interface IEventBusSubcriptionManager
    {
        bool IsEmpty { get; }
        event EventHandler<string> OnEventRemoved;
        
        void AddSubscription<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>;
        void AddDynamicSubcription<TH>(string eventName) where TH : IDynamicIntegrationEventHandler;
        void RemoveSubscription<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>;
        void RemoveDynamicSubscription<TH>(string eventName) where TH : IDynamicIntegrationEventHandler;

        bool HasSubscriptionsForEvent(string eventName);
        bool HasSubscriptionsForEvent<T>() where T : IntegrationEvent;
        Type GetEventTypeByName(string eventName);

        IEnumerable<SubscriptionInfo> GetHandlersForEvent<T>() where T : IntegrationEvent;
        IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName);
        string GetEventKey<T>();

        void Clear();

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EventBusSystem
{
    public static class EventBus
    {
        private static Dictionary<Type, SubscribersList<IGlobalSubscriber>> s_Subscribers
            = new Dictionary<Type, SubscribersList<IGlobalSubscriber>>();

        public static void Subscribe(IGlobalSubscriber subscriber)
        {
            List<Type> subscriberTypes = EventBusHelper.GetSubscriberTypes(subscriber);
            foreach (Type t in subscriberTypes)
            {
                if (!s_Subscribers.ContainsKey(t))
                {
                    s_Subscribers[t] = new SubscribersList<IGlobalSubscriber>();
                }

                s_Subscribers[t].Add(subscriber);
            }
        }

        public static void Unsubscribe(IGlobalSubscriber subscriber)
        {
            List<Type> subscriberTypes = EventBusHelper.GetSubscriberTypes(subscriber);
            foreach (Type t in subscriberTypes)
            {
                if (s_Subscribers.ContainsKey(t))
                {
                    s_Subscribers[t].Remove(subscriber);
                }
            }
        }

        public static void RaiseEvent<TSubscriber>(Action<TSubscriber> action)
            where TSubscriber : class, IGlobalSubscriber
        {
            if (!s_Subscribers.ContainsKey(typeof(TSubscriber))) return;
            SubscribersList<IGlobalSubscriber> subscribers = s_Subscribers[typeof(TSubscriber)];
            foreach (IGlobalSubscriber subscriber in subscribers.List.ToList())
            {
                try
                {
                    action?.Invoke(subscriber as TSubscriber);
                }
                catch (Exception e)
                {
                    Debug.LogWarning($"EventBus event error {e.Message} {subscriber}");
                }
            }

            subscribers.Executing = false;
            subscribers.Cleanup();
        }
    }
}
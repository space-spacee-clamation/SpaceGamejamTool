using System;
using System.Collections.Generic;
namespace  Space.EventFramework
{
    
    /// <summary>
    /// 使用delegate而不是直接用Action 是为了保证 in关键字
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public delegate void GameEventDelegate<T> (in T data) where T : IEventData;
    /// <summary>
    /// 事件总栈
    /// </summary>
    public static class EventBus
    {
        /// <summary>
        /// 只是方便存储的接口
        /// </summary>
        private interface IEventBusSubscriber
        {
            
        }
        /// <summary>
        /// 某个事件的分发器
        ///避免拆装箱
        /// </summary>
        private class EventBusSubscriber<T> : IEventBusSubscriber where T : IEventData
        {
            GameEventDelegate<T> handlers ;
            public void Subscribe(GameEventDelegate<T> handler)
            {
                handlers+=handler;
            }
            public void Unsubscribe(GameEventDelegate<T> handler)
            {
                handlers -= handler;
            }
            public void Publish(in T eventData)
            {
                handlers.Invoke(eventData);
            }
        }
        /// <summary>
        /// 事件字典
        /// </summary>
        private static Dictionary<Type, IEventBusSubscriber> _subscribers = new Dictionary<Type, IEventBusSubscriber>();
        /// <summary>
        /// 注册订阅
        /// </summary>
        /// <param name="handler">执行的事件</param>
        /// <typeparam name="T"></typeparam>
        public static void Subscribe<T>(GameEventDelegate<T> handler)  where T : IEventData
        {
            var eventType = typeof(T);
            if (!_subscribers.ContainsKey(eventType))
                _subscribers.Add(eventType ,new EventBusSubscriber<T>());
            (_subscribers[eventType] as EventBusSubscriber<T>)?.Subscribe( handler);
        }
        /// <summary>
        /// 注销事件
        /// </summary>
        public static void Unsubscribe<T>(GameEventDelegate<T> handler) where T : IEventData
        {
            var eventType = typeof(T);
            if (!_subscribers.TryGetValue(eventType, out IEventBusSubscriber subscriber))
                return;
            (subscriber as EventBusSubscriber<T>)?.Unsubscribe(handler);
        }
        /// <summary>
        /// 发布事件
        /// </summary>
        /// <param name="eventData">事件数据 使用in关键字减少复制，同时防止修改</param>
        /// <typeparam name="T">事件Type</typeparam>
        public static void Publish<T>(in T eventData)  where T : IEventData
        {
            var eventType = typeof(T);
            if (!_subscribers.ContainsKey(eventType))
                return;
            (_subscribers[eventType] as EventBusSubscriber<T>)?.Publish(eventData);
        }
    }
}
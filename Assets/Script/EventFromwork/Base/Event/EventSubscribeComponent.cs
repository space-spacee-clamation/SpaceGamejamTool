using System;
using System.Collections.Generic;
using Space.EventFramework.BaseEvent;
using UnityEngine;
namespace Space.EventFramework
{
    /// <summary>
    /// 物体上绑定的事件注册者
    /// 实体通过这个组件去注册事件而不是直接调用bus
    /// 注意 :  如果没有绑定此组件就不会调用销毁的事件，可能需要手动添加
    /// </summary>
    public  class EventSubscribeComponent : MonoBehaviour
    {
        /// <summary>
        /// 存储接口
        /// 方便存
        /// </summary>
        private interface IEventSubscriber
        {
            Type EventType { get; }
            void Clear();
        }
        /// <summary>
        /// 同事件总栈的处理方法
        /// 使用内部类进行封装减少开销
        /// 存储所有这个物体的事件
        /// 注册和注销都通过这个类进行
        /// </summary>
        /// <typeparam name="T">事件类型</typeparam>
        private class EventSubscriber<T> : IEventSubscriber where T : IEventData
        {
            public Type EventType => typeof(T);
            private GameEventDelegate<T> Handel;
            /// <summary>
            /// 事件计数器，类似智能指针，没有事件自动销毁
            /// </summary>
            private int _counter=0;
            private EventSubscribeComponent _owner;
            public EventSubscriber(EventSubscribeComponent owner,GameEventDelegate<T> handler)
            {
                _owner = owner;
                Subscribe( handler);
            }
            public void Subscribe(GameEventDelegate<T> handler)
            {
                Handel += handler;
                _counter++;
                EventBus.Subscribe(handler);
            }
            public void UnSubscribe(GameEventDelegate<T> handler)
            {
                Handel -= handler;
                _counter--;
                EventBus.Unsubscribe(handler);
                if (_counter <= 0)
                    _owner.UnregisterEventHandlers(this);
            }
            public void Clear()
            {
                EventBus.Unsubscribe(Handel);
                Handel = null;
                _counter = 0;
            }
        }
        /// <summary>
        /// 物体的事件
        /// </summary>
        private Dictionary<Type,IEventSubscriber> _eventSubscribers=new Dictionary<Type, IEventSubscriber>();
        protected void OnEnable()
        {
            // 自动绑定生命周期事件
            EventBus.Subscribe<GameObjectDestroyedEvent>(OnOwnerDestroyed);
        }
        private void OnDestroy()
        {
            EventBus.Publish(new GameObjectDestroyedEvent(gameObject) );
            EventBus.Unsubscribe<GameObjectDestroyedEvent>(OnOwnerDestroyed);
        }
        public void Subscribe<T>(GameEventDelegate<T> handler) where T : IEventData
        {
            //TODO: 加入debug模式，可以对当个物体的事件出入进行debug
            if  (!_eventSubscribers.ContainsKey(typeof(T)))
                _eventSubscribers.Add(typeof(T), new EventSubscriber<T>(this, handler));
            else
            {
               ( _eventSubscribers[typeof(T)]as EventSubscriber<T>)?.Subscribe(handler);
            }
        }
        private void OnOwnerDestroyed(in GameObjectDestroyedEvent e)
        {
            if (e.ObjectInstance == gameObject)
            {
                foreach (var subscriber in _eventSubscribers.Values)
                {
                    // 自动解除注册
                    UnregisterEventHandlers(subscriber);
                }
            }
        }
        private void UnregisterEventHandlers(IEventSubscriber subscriber)
        {
            subscriber.Clear();
        }
        public void Publish<T>(in T data) where T : IEventData
        {
            //TODO: 加入debug模式，可以对当个物体的事件出入进行debug
            EventBus.Publish(data);
        }
    }
}
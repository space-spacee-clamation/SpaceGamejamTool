using Script;
using Space.EventFramework.BaseEvent;
using Space.GlobalInterface.EventInterface;
using UnityEngine;
namespace Space.EventFramework
{
    public class MonoEventSubComponent : MonoBehaviour
    {
        ITypeEventComponent _typeEventSubscribeComponent;
        private void Awake()
        {
            _typeEventSubscribeComponent =FrameworkFactory.GetInstance<ITypeEventComponent>();
            _typeEventSubscribeComponent.BindBus(GlobalEventBus.Instance);
        }
        protected void OnEnable()
        {
            // 自动绑定生命周期事件
            _typeEventSubscribeComponent.Subscribe<GameObjectDestroyedEvent>(OnOwnerDestroyed);
        }
        private void OnDestroy()
        {
            _typeEventSubscribeComponent.Publish(new GameObjectDestroyedEvent(gameObject) );
            _typeEventSubscribeComponent.UnSubscribe<GameObjectDestroyedEvent>(OnOwnerDestroyed);
        }
        
        private void OnOwnerDestroyed(in GameObjectDestroyedEvent e)
        {
            if (e.ObjectInstance == gameObject)
            {
                _typeEventSubscribeComponent.Clear();
            }
        }
        public void Subscribe<T>(GameEventDelegate<T> handler) where T : IEventData
        {
            _typeEventSubscribeComponent.Subscribe( handler);
        }
        public void UnSubscribe<T>(GameEventDelegate<T> handler) where T : IEventData
        {
            _typeEventSubscribeComponent.UnSubscribe( handler);
        }
        public void Clear()
        {
            _typeEventSubscribeComponent.Clear();
        }
        public void Publish<T>(in T data) where T : IEventData
        {
            _typeEventSubscribeComponent.Publish( data);
        }
    }
}
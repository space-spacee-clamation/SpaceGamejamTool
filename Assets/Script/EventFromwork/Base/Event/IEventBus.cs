namespace Space.EventFramework
{
    /// <summary>
    /// 事件收发的接口
    /// </summary>
    public interface IEventBus
    {
        public  void Subscribe<T>(GameEventDelegate<T> handler)  where T : IEventData;
        public  void Unsubscribe<T>(GameEventDelegate<T> handler) where T : IEventData;
        public  void Publish<T>(in T eventData)  where T : IEventData;
    }
}
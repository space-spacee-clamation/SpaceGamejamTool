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
    public static class GlobalEventBus
    {
        public static IEventBus Instance = new EventBus();
    }
}
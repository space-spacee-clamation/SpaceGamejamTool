using System;
using System.Collections.Generic;
using Space.GlobalInterface;
namespace  Space.EventFramework
{
   
    /// <summary>
    /// 事件总栈
    /// </summary>
    public static class GlobalEventBus
    {
        public static IEventBus Instance = new EventBus();
    }
}
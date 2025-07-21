using System;
using System.Collections.Generic;
namespace Space.EventFramework
{
    
    //TODO: 完善黑板
    
    /// <summary>
    /// 游戏全局黑板
    /// TODO : 需要接入生命周期吗🤔?
    /// </summary>
    public class GameBlackBord
    {
        public static GameBlackBord Instance = new GameBlackBord();
        public Dictionary<string, IBlackBordItem> BlackBordItems = new Dictionary<string, IBlackBordItem>();
        
        public void Set(string key, object value)
        { 
            if (!BlackBordItems.ContainsKey(key))
            {
                BlackBordItems.Add(key, new BlackBordItem(value));
            }
        }
        public object Get(string key)
        {
            return BlackBordItems[key].value;
        }
    }
    public interface IBlackBordItem
    {
        public delegate void OnValueChangeHandler(object oldValue, object newValue);

        public void SubValueChange(OnValueChangeHandler  handler);
        
        object value { get; }
    }
    public class BlackBordItem : IBlackBordItem
    {
        private IBlackBordItem.OnValueChangeHandler OnValueChangeHandler;
        public BlackBordItem(object value)
        {
            this.value = value;
        }
        public void SubValueChange(IBlackBordItem.OnValueChangeHandler handler)
        {
            OnValueChangeHandler+= handler;
        }
        public object value {
            get;
            private set;
        }
        public void SubValueChange()
        {
            
        }
    }
}
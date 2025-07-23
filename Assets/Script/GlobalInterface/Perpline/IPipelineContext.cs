namespace Space.GlobalInterface.PipelineInterface
{
    /// <summary>
    /// 管线使用的数据接口，标记用暂时无其它作用
    /// </summary>
    public interface IPipelineContext
    {
        /// <summary>
        /// 共享数据存储
        /// </summary>
        /// <param name="key"></param>
        object GetSharedData(string key);
        /// <summary>
        /// 数据存储
        /// </summary>
        void SetSharedData(string key, object value);
        
        bool TryGetSharedData(string key, out object value);
        
        bool TryGetSharedData<T>(string key, out T value);  
        
        T  GetSharedData<T>(string key);
        
        bool  TrySetSharedData(string key, object value);
        
        bool ContainsKey(string key);
        
        //上下文状态
        //TODO: 保留关键字目前没作用
        string PipelineStatus { get; set; }
        /// <summary>
        /// 删除上下文
        /// 便于重复使用
        /// </summary>
        void Clear();
    }
}
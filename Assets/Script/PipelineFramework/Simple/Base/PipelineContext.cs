using System.Collections.Generic;
using Space.GlobalInterface.PipelineInterface;
namespace Space.PipelineFramework.Simple
{
    public class PipelineContext : IPipelineContext
    {
        private Dictionary<string,object> parameters;
        public PipelineContext()
        {
            PipelineStatus = PipelineStatusEnum.DEFAULT;
            parameters=new Dictionary<string, object>();
        }
        public object GetSharedData(string key)
        {
            if (parameters.ContainsKey(key))
            return parameters[key];
            else
            {
                throw new KeyNotFoundException();
            }
        }
        public void SetSharedData(string key, object value)
        {
            parameters.Add(key,value);
        }
        public bool TryGetSharedData(string key, out object value)
        {
            return parameters.TryGetValue(key,out value);
        }
        public bool TryGetSharedData<T>(string key, out T value) 
        {
            object tempValue;
            bool success = TryGetSharedData(key,out tempValue);
            if (success)
                value = (T)tempValue;
            else
                value = default(T);
            return success;
        }
        public T GetSharedData<T>(string key) 
        {
            object tempValue;
            bool success = TryGetSharedData(key,out tempValue);
            if (success)
                return (T)tempValue;
            else
                return default(T);
        }
        public bool TrySetSharedData(string key, object value)
        {
            return parameters.TryAdd(key,value);
        }
        public bool ContainsKey(string key)
        {
            return parameters.ContainsKey(key);
        }
        public string PipelineStatus {
            get;
            set;
        }
        public void Clear()
        {
            parameters.Clear();
        }
    }
    public class PipelineStatusEnum
    {
        public const string SUCCESS = "Success";
        public const string DEFAULT = "Default";
        public const string FAILED = "Failed";
    }
}
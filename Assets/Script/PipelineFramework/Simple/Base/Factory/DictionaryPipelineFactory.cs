using System;
using System.Collections.Generic;
using System.Reflection;
using Space.GlobalInterface.PipelineInterface;
using Space.PipelineFramework.Simple.Example.Test;
using Space.Utility;
namespace Space.PipelineFramework.Simple
{
    /// <summary>
    /// 不使用属性，通过两个字符串配置的工厂
    ///所有工厂都是用全局初始化的数据，可能导致管道会需要提前配置，这个算是dictionary的工厂缺点
    /// TODO: 怎么解决配置的问题
    /// </summary>
    public class DictionaryPipelineFactory : IPipelineFactory
    {
        private static Dictionary<string,IPipelineStage> pipelineMeta;
        //TODO: 配置文件
        /// <summary>
        /// 第一项是name 第二项是type的fullname
        /// </summary>
        List<(string,string)> pipeStageConfList=new List<(string,string)>()
        {
            ("Example/Test/TestParamsPipeline",typeof(TestParamsPipeline).FullName),
            ("Example/Test/TestStage",typeof(TestPipelineStage).FullName),
        };
        
        
        private static bool initialized = false;
        private static object locker = new object();
        public DictionaryPipelineFactory()
        {
            if (initialized)
                return;
            lock (locker)
            {
                if(initialized) return;
                pipelineMeta=new Dictionary<string, IPipelineStage>();
                InitFactory();
            }
        }
        private void InitFactory()
        {
            foreach (var confige in pipeStageConfList)
            {
                IPipelineStage pipelineStage;
                try
                {
                   var resType=Type.GetType(confige.Item2);
                   pipelineStage = ReflectionUtility.CreateInstance(resType) as IPipelineStage;
                }
                catch
                {
                    throw new Exception($"尝试获取Type :{confige.Item2}出现错误，请检测配置文件");
                }
                pipelineMeta.Add(confige.Item1, pipelineStage);
            }
        }
        public IPipelineStage CreatePipelineStage(string name)
        {
            return pipelineMeta[name].Clone();
        }
        public IPipelineStage CreatePipelineStage(string name, params object[] pipelineParams)
        {
            var temp= pipelineMeta[name].Clone();
            return temp.SetParams(pipelineParams);
        }
    }
}
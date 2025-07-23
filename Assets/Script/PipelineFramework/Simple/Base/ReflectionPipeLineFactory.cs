using System;
using System.Collections.Generic;
using System.Reflection;
using Space.GlobalInterface.PipelineInterface;
using Space.Utility;
namespace Space.PipelineFramework.Simple
{
    
    [AttributeUsage(AttributeTargets.Class)]
    public class PipelineStageAttribute : Attribute
    {
        public string Name;
        public Type Type;
        public PipelineStageAttribute(string name,Type stageType)
        {
            this.Name = name;
            Type=stageType;
        }
    }
    
    //TODO：可以加个泛型方法
    public class ReflectionPipeLineFactory : IPipelineFactory
    {
        private static Dictionary<string,IPipelineStage> pipelineMeta;
        private static List<string> AssemblyName = new List<string>()
        {
          
        };
        private static bool initialized = false;
        private static object locker = new object();
        public ReflectionPipeLineFactory()
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
            LoadStage( ReflectionUtility.GetAttributesFromExecutingAssembly<PipelineStageAttribute>());
            if(AssemblyName.Count>0)
                LoadStage(ReflectionUtility.GetAttributesFromAssemblies<PipelineStageAttribute>(AssemblyName));
        }
        private void LoadStage(IEnumerable< PipelineStageAttribute> attributes)
        {
            foreach (var attribute in attributes)
            {
                pipelineMeta.Add(attribute.Name,ReflectionUtility.CreateInstance(attribute.Type) as IPipelineStage);
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
using Space.GlobalInterface.PipelineInterface;
using UnityEngine;
namespace Space.PipelineFramework.Simple.Example.Test
{
    /// <summary>
    /// 此处用了Attribute 可以通过ReflectionPipeLineFactory调用
    /// 也可也用DictionaryFactory 看喜好选择
    /// </summary>
    [PipelineStage("Example/Test/TestParamsPipeline",typeof(TestPipelineStage))]
    public class TestParamsPipeline : APipelineStage<TestParamsPipeline>
    {
        private string addedMassage= "";
        public class TestParamsPipelineCreateInfo
        {
            public int DefaultPriority;
            public string AddedMassage;
        }
        public override IPipelineStage SetParams(params object[] parameters)
        {
            TestParamsPipelineCreateInfo info= parameters[0]  as TestParamsPipelineCreateInfo;
            this.DefaultPriority= info.DefaultPriority;
            addedMassage = info.AddedMassage;
            return base.SetParams(parameters);
        }
        private int index=0;
        public override void Execute(IPipelineContext context)
        {
            if (context.TryGetSharedData("ContextIndex", out index)) ;
            string res= context.GetSharedData("TestCount" )as string;
            Debug.Log($"{index}: {res}  ---- {addedMassage}");
            context.SetSharedData("ContextIndex",++index);
        }
    }
}
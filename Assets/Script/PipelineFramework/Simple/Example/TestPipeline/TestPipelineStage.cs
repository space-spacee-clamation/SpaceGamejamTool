using Space.GlobalInterface.PipelineInterface;
using UnityEngine;
namespace Space.PipelineFramework.Simple.Example.Test
{

    public class TestContest
    {
        public string massage;
        public  int index;
    }
        [PipelineStage("Example/Test/TestStage",typeof(TestPipelineStage))]
    public class TestPipelineStage : APipelineStage<TestPipelineStage>
    {
        public int Priority {
            get;
        }
        private int index=0;
        public override void Execute(IPipelineContext context)
        {
            if (context.TryGetSharedData("ContextIndex", out index)) ;
            string res= context.GetSharedData("TestCount" )as string;
            Debug.Log($"{index}: {res}");
            context.SetSharedData("ContextIndex",++index);
        }
    }
}
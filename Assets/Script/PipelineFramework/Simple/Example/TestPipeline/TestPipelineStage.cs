using Space.GlobalInterface.PipelineInterface;
using UnityEngine;
namespace Space.PipelineFramework.Simple.Example.Test
{

    public class TestContest
    {
        public string massage;
        public  int index;
    }
        [PipelineStage("Example.Test.TestStage",typeof(TestPipelineStage))]
    public class TestPipelineStage : APipelineStage<TestPipelineStage>
    {
        public int Priority {
            get;
        }
        public override void Execute(IPipelineContext context)
        {
            TestContest res= context.GetSharedData("Test") as TestContest;
            Debug.Log($"{res.index} : {res.massage}");
            res.index++;
        }
    }
}
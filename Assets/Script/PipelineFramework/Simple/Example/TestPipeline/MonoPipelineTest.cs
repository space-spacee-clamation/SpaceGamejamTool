using System;
using Space.GlobalInterface.PipelineInterface;
using UnityEngine;
namespace Space.PipelineFramework.Simple.Example.Test
{
    public class MonoPipelineTest : MonoBehaviour
    {
        private IPipeline pipeline;
        private IPipelineContext pipelineContext;
        private void Awake()
        {
            IPipelineFactory factory = GlobalPipelineStageFactory.Instance;
            pipeline = FrameworkFactory.GetInstance<IPipeline>();
            pipeline.AddStage(factory.CreatePipelineStage("Example/Test/TestStage"));
            pipeline.AddStage(factory.CreatePipelineStage("Example/Test/TestStage"));
            pipeline.AddStage(factory.CreatePipelineStage("Example/Test/TestStage"));
            pipeline.AddStage(factory.CreatePipelineStage("Example/Test/TestStage"));
            pipelineContext = FrameworkFactory.GetInstance<IPipelineContext>();
        }


        private float timer = 0;
        private int count = 0;
        private void Update()
        {
            timer-=Time.deltaTime;
            if (timer<0)
            {
                pipelineContext.Clear();
                pipelineContext.SetSharedData("TestCount", $"{count++}次管道测试");
                pipeline.Execute(pipelineContext);
                timer = 3f;
            }
        }
    }
}
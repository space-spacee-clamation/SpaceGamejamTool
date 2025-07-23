using System.Collections.Generic;
using Space.GlobalInterface.PipelineInterface;
namespace Space.PipelineFramework.Simple.Example.DamagePipeLine
{
/// <summary>
/// 造成伤害的信息
/// </summary>

    /// <summary>
    /// 封装了一层具有默认管线的管道
    /// </summary>
    public class DamagePipeLine : IPipeline
    {
        IPipeline pipeline;
        public DamagePipeLine()
        {
            var factory = GlobalPipelineStageFactory.Instance;
            pipeline = FrameworkFactory.GetInstance<IPipeline>();
            pipeline.AddStage(factory.CreatePipelineStage(""));
        }
        public void AddStage(IPipelineStage stage)
        {
            pipeline.AddStage(stage);
        }
        public void RemoveStage(IPipelineStage stage)
        {
            pipeline.RemoveStage(stage);
        }
        public void Execute(IPipelineContext context)
        {
            pipeline.Execute(context);   
        }
    }
}
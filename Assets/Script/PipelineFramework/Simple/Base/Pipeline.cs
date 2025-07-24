using System.Collections;
using Space.Collections;
using Space.GlobalInterface.PipelineInterface;
namespace Space.PipelineFramework.Simple
{
    /// <summary>
    /// 线性的pipeline 
    /// </summary>
    public class Pipeline : IPipeline
    {
        SortedList<int , IPipelineStage> pipelineStages = new SortedList<int , IPipelineStage>();
        public void AddStage(IPipelineStage stage)
        {
            pipelineStages.Add(stage.DefaultPriority, stage);
        }
        public void AddStage(IPipelineStage stage, int priority)
        {
            pipelineStages.Add(priority, stage);
        }
        public void RemoveStage(IPipelineStage stage)
        {
            pipelineStages.Remove(stage);
        }
        public void Execute(IPipelineContext context)
        {
            foreach (var pipelineStage in pipelineStages.GetSorted())
            {
                pipelineStage.Execute(context);
            }
        }
    }
}
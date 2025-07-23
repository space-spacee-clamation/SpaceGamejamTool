using Space.GlobalInterface.PipelineInterface;
namespace Space.PipelineFramework.Simple.Example.DamagePipeLine
{
    /// <summary>
    /// 基础伤害管道
    /// </summary>
    [PipelineStage("Example/DamagePipeLine/BaseDamageCalculate",typeof(BaseDamageCalculate))]
    public class BaseDamageCalculate : APipelineStage<BaseDamageCalculate>
    {
        public override void Execute(IPipelineContext context)
        {
            
        }
    }
}
namespace Script.GlobalInterface.PipelineInterface
{
    /// <summary>
    /// 管线的组成部分
    /// 执行实际的处理逻辑
    /// </summary>
    public interface IPipelineStage<T>  where T : IPipelineContext
    {
        
    }
}
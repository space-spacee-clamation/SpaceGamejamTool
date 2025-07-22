namespace Script.GlobalInterface.PipelineInterface
{
    /// <summary>
    /// 数据处理管线
    /// 或者什么其它管线
    /// 进行管线的链接和其中数据的传递
    /// </summary>
    public interface IPipeline<T> where T : IPipelineContext
    {
        
    }
}
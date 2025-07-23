namespace Space.GlobalInterface.PipelineInterface
{
    /// <summary>
    /// 管道流水线的工厂
    /// 通过字符串索引获得IPipelineStage
    /// </summary>
    public interface IPipelineFactory
    {
        /// <summary>
        /// 通过名字索引新建pipelineStage
        /// </summary>
        /// <param name="name">名字</param>
        public IPipelineStage CreatePipelineStage(string name);
    }
}
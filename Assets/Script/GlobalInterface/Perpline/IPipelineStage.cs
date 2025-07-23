namespace Space.GlobalInterface.PipelineInterface
{
    /// <summary>
    /// 管线的组成部分
    /// 执行实际的处理逻辑
    /// </summary>
    public interface IPipelineStage
    {
        public int Priority { get; }
        
        public IPipelineStage SetParams(params object[] parameters);
        /// <summary>
        /// 触发管道
        /// </summary>
        /// <param name="context">数据上下文</param>
        public void Execute(IPipelineContext context);
        public IPipelineStage Clone();
    }
}
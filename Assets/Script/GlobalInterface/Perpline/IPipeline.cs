namespace Space.GlobalInterface.PipelineInterface
{
    /// <summary>
    /// 数据处理管线
    /// 或者什么其它管线
    /// 进行管线的链接和其中数据的传递
    /// </summary>
    public interface IPipeline
    {
        public void AddStage(IPipelineStage stage);
        public void RemoveStage(IPipelineStage stage);
        /// <summary>
        /// </summary>
        /// <returns>
        /// 返回的string 可以是管道运行的情况
        ///比方说sucsess
        /// </returns>
        public void Execute(IPipelineContext context);
    }
}
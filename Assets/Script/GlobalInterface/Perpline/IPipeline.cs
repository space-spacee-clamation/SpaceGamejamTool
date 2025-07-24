namespace Space.GlobalInterface.PipelineInterface
{
    /// <summary>
    /// 数据处理管线
    /// 或者什么其它管线
    /// 进行管线的链接和其中数据的传递
    /// </summary>
    public interface IPipeline
    {
        /// <summary>
        /// 增加管道的组件
        /// 如果没有传入优先级会使用默认优先级
        /// </summary>
        public void AddStage(IPipelineStage stage);
        /// <summary>
        /// 增加管道的组件
        /// 如果没有传入优先级会使用默认优先级
        /// </summary>
        public void AddStage(IPipelineStage stage,int priority);

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
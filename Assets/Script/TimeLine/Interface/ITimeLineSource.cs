namespace Space.TimelineFramework
{
    /// <summary>
    /// Source负责提供生成frame的接口，
    /// </summary>
    public interface ITimeLineSource<T> where T : ITimeLineFrame
    {
        public T CreateFrame();
        /// <summary>
        /// 插值应用，因为frame和time不一定是一一对应的
        /// </summary>
        /// <param name="timeAlpha">range [0,1]</param>
        public void ApplayFrame(T preFrame, T nextFrame, float timeAlpha);
    }
}

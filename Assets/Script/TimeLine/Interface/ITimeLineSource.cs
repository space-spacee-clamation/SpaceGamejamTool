namespace Space.TimelineFramework
{
    /// <summary>
    /// Source负责提供生成frame的接口，
    /// </summary>
    public interface ITimeLineSource<T> where T : ITimeLineFrame
    {
        public T CreateFrame(ITimeTick timeTick);
        public void ApplayFrame(T preFrame, T nextFrame, ITimeTick timeTick);
    }
}

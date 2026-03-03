namespace Space.TimelineFramework
{
    public interface ITimeLineLearpTool<T> where T : ITimeLineFrame
    {
        public T Lerp(T preFrame, T nextFrame, float timeAlpha);
    }
}

namespace Space.TimelineFramework
{
    /// <summary>
    /// 负责提供Track的接口，类似工厂
    /// </summary>
    public interface ITimeLineTrackProvider
    {
        public void CreateTrack(in ITimeLine timeLine);
    }
}

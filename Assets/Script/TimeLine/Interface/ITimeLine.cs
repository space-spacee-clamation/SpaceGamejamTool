namespace Space.TimelineFramework
{
    /// <summary>
    /// timeline的轨道提供者，timeline的核心概念之一，timeline由多个track组成，track由多个frame组成
    /// track负责管理frame的生命周期，frame负责执行具体的逻辑
    /// </summary>
    public interface ITimeLine
    {
        public void AddTrack(ITimeLineTrack track);
        public void RemoveTrack(ITimeLineTrack track);
        public void ClearTrack();
        public void BindTimer(ITimeTick timer);
    }
}

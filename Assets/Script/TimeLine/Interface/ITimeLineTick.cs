namespace Space.TimelineFramework
{
    /// <summary>
    /// TimeLine的核心，内部管理回调的时机
    /// TimeTick 只标示tick的变化  不包含time的概念
    /// </summary>
    public interface ITimeLineTick
    {
        public int GetCurrentTick();
        public void ResetTick();
        public void ResetTickTo(int tick);
        public int TickScale { get; set; }
    }
}

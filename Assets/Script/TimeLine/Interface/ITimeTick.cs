namespace Space.TimelineFramework
{
    /// <summary>
    /// TimeLine的核心，内部管理回调的时机
    /// </summary>
    public interface ITimeTick
    {
        public int GetCurrentTick();
        public float GetTime();
        public void ResetTick();
        public void ResetTickTo(int tick, float time);
        public float TickScale { get; set; }
    }
}

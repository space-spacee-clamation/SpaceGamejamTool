namespace Space.TimelineFramework
{
    public class TimeTick : ITimeLineTick
    {
        private int currentTick = 0;
        public int TickScale { get; set; } = 1;
        public int GetCurrentTick()
        {
            return currentTick;
        }
        public void ResetTick()
        {
            currentTick = 0;
        }
        public void ResetTickTo(int tick)
        {
            currentTick = tick;
        }
        public void Tick()
        {
            currentTick += TickScale;
        }
    }
}

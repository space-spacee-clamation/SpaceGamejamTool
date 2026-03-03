using System.Collections.Generic;

namespace Space.TimelineFramework
{
    /// <summary>
    /// 时间轴的核心类，提供时间轴的基本功能
    /// </summary>
    public class TimeLine : ITimeLine
    {
        protected ITimeLineTick timeTick;
        protected List<ITimeLineTrack> tracks = new List<ITimeLineTrack>();
        public TimeLine(ITimeLineTick timeTick)
        {
            this.timeTick = timeTick;
        }
        public void AddTrack(ITimeLineTrack track)
        {
            tracks.Add(track);
            track.BindeTick(timeTick);
        }
        public void BindTimer(ITimeLineTick timer)
        {
            timeTick = timer;
            foreach (var track in tracks)
            {
                track.BindeTick(timer);
            }
        }
        public void ClearTrack()
        {
            tracks.Clear();
        }
        public void RemoveTrack(ITimeLineTrack track)
        {
            tracks.Remove(track);
        }
        public void Tick()
        {
            foreach (var track in tracks)
            {
                track.Tick();
            }
        }
    }
}

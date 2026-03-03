using System.Collections.Generic;

namespace Space.TimelineFramework
{
    public class TimeLineTrack<T> : ITimeLineTrack where T : ITimeLineFrame
    {
        public TimeLineTrack(ITimeLineFrameSource<T> timeLineFrameSource, params ITimeLineEffect<T>[] timeLineEffect)
        {
            _timeLineFrameSource = timeLineFrameSource;
            _timeLineEffect = timeLineEffect;
            _timelineContainer = new TimelineContainer<T>(timeLineFrameSource, _timeLineTick);
        }
        public void AddEffect(ITimeLineEffect<T> effect)
        {
            var list = new List<ITimeLineEffect<T>>(_timeLineEffect)
            {
                effect
            };
            _timeLineEffect = list.ToArray();
        }
        public int FrameCount => _timeLineFrameSource.FrameCount;
        private ITimeLineFrameSource<T> _timeLineFrameSource;
        private TimelineContainer<T> _timelineContainer;
        private ITimeLineEffect<T>[] _timeLineEffect;
        private ITimeLineTick _timeLineTick;
        private int preFrameIndex = -1;
        public bool IsLoop { get; set; } = false;
        public void BindeTick(ITimeLineTick timeTick)
        {
            _timeLineTick = timeTick;
        }

        public void Tick()
        {
            _timelineContainer.Tick(Effect);
        }
        protected void Effect(T frame)
        {
            foreach (var effect in _timeLineEffect)
            {
                effect.ApplayFrame(frame);
            }
        }
    }
}

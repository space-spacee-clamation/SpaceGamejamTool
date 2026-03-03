using System;

namespace Space.TimelineFramework
{
    /// <summary>
    ///帮助进行有效帧记录，帧应用，帧插值等
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TimelineContainer<T> where T : ITimeLineFrame
    {
        private ITimeLineFrameSource<T> _timeLineFrameSource;
        private ITimeLineLearpTool<T> _lerpTool;
        private T _preEffectFrame;
        private T _nextEffectFrame;
        private int _preEffectTickeIndex = -1;
        private int _nextEffectFrameTick = -1;
        private ITimeLineTick _timeLineTick;
        public TimelineContainer(ITimeLineFrameSource<T> source, ITimeLineTick timeLineTick)
        {
            _timeLineFrameSource = source;
            _preEffectFrame = default;
            _nextEffectFrame = default;
            _preEffectTickeIndex = 0;
            _nextEffectFrameTick = 0;
            _timeLineTick = timeLineTick;
            _lerpTool = source.GetLerpTool();
        }
        public void Tick(Action<T> applyFrame)
        {
            var currentTick = _timeLineTick.GetCurrentTick();
            if (currentTick >= _nextEffectFrameTick)
            {
                while (currentTick >= _nextEffectFrameTick)
                {
                    applyFrame.Invoke(_nextEffectFrame);
                    _preEffectFrame = _nextEffectFrame;
                    _preEffectTickeIndex = _nextEffectFrameTick;
                    _nextEffectFrame = _timeLineFrameSource.GetNextEffectFrame(_nextEffectFrameTick, out _nextEffectFrameTick);
                }
            }
            if (_lerpTool != null)
            {
                applyFrame.Invoke(
                    _lerpTool.Lerp(_preEffectFrame, _nextEffectFrame,
                    (currentTick - _preEffectTickeIndex) / (float)(_nextEffectFrameTick - _preEffectTickeIndex)));
            }
        }
    }
}

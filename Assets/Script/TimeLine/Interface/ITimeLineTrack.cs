using System.Collections.Generic;

namespace Space.TimelineFramework
{
    /// <summary>
    /// timeline的轨道抽象， timeline的核心概念之一，timeline由多个track组成，track由多个frame组成
    /// track负责管理frame的生命周期，frame负责执行具体的逻辑
    /// 这个接口是向外部调用使用的，不需要存数据类型是因为数据不会向外流出
    /// </summary>
    public interface ITimeLineTrack
    {
        int FrameCount { get; }
        float TotalTime { get; }
        bool IsLoop { get; set; }
        void ApplyInterpolated(ITimeTick timeTick);
    }
}

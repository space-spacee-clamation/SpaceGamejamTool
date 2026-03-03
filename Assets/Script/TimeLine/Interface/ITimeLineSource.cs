using System.Collections.Generic;
using Space.TimelineFramework.Sample;

namespace Space.TimelineFramework
{
    /// <summary>
    /// Effect负责应用frame的接口
    /// 是timeline的效果器
    /// </summary>
    public interface ITimeLineEffect<T> where T : ITimeLineFrame
    {
        /// <summary>
        /// 直接应用
        /// </summary>
        /// <param name="frame"></param>
        public void ApplayFrame(T frame);
        /// <summary>
        /// 插值应用，因为frame和time不一定是一一对应的
        /// </summary>
        /// <param name="timeAlpha">range [0,1]</param>
        public void ApplayFrame(T preFrame, T nextFrame, float timeAlpha);
    }
    /// <summary>
    ///FrameSource负责获取frame的接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITimeLineFrameSource<T> where T : ITimeLineFrame
    {
        /// <summary>
        /// 创建frame
        /// 可能会创建多个frame，frame和time不一定是一一对应的
        /// </summary>
        public IList<T> GetFrames();
        /// <summary>
        /// 获取tick范围内的frame
        /// [tickfrom, tickto)
        /// </summary>
        public IList<T> GetTickFrame(int tickform, int tickto);
        /// <summary>
        /// 获取frame
        /// </summary>
        public T GetFrame(int tick);
        /// <summary>
        /// 获取tick下一个有效值
        /// </summary>
        public T GetNextEffectFrame(int tick, out int nextTick);
        public ITimeLineLearpTool<T> GetLerpTool();
        public int FrameCount { get; }
    }
}

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
        /// 应用如果需要引用插值的效果应该是传入插值后的frame而不是在系统内插值
        /// </summary>
        /// <param name="frame"></param>
        public void ApplayFrame(T frame);
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

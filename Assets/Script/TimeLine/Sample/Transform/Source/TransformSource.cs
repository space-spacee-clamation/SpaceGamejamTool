using System.Collections.Generic;

namespace Space.TimelineFramework.Sample
{
    /// <summary>
    /// TransformSource 只是一个简单的例子  代表一个物体的位移旋转缩放
    /// </summary>
    public class TransformSource : ITimeLineFrameSource<PositionFrame>
    {
        public int FrameCount => throw new System.NotImplementedException();

        public PositionFrame GetFrame(int tick)
        {
            throw new System.NotImplementedException();
        }

        public IList<PositionFrame> GetFrames()
        {
            throw new System.NotImplementedException();
        }

        public ITimeLineLearpTool<PositionFrame> GetLerpTool()
        {
            throw new System.NotImplementedException();
        }

        public PositionFrame GetNextEffectFrame(int tick, out int nextTick)
        {
            throw new System.NotImplementedException();
        }

        public IList<PositionFrame> GetTickFrame(int tickform, int tickto)
        {
            throw new System.NotImplementedException();
        }
    }
}

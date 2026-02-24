using UnityEngine;

namespace Space.TimelineFramework.Sample
{
    public struct PositionFrame : ITimeLineFrame
    {
        public Vector3 Position;

    }
    public struct RotationFrame : ITimeLineFrame
    {
        public Quaternion Rotation;
    }
    public class PositionSource : ITimeLineSource<PositionFrame>
    {
        private Transform _transform;
        public PositionSource(Transform transform)
        {
            _transform = transform;
        }
        public void ApplayFrame(PositionFrame preFrame, PositionFrame nextFrame, float timeAlpha)
        {
            _transform.position = Vector3.Lerp(preFrame.Position, nextFrame.Position, timeAlpha);
        }
        public PositionFrame CreateFrame()
        {
            return new PositionFrame() { Position = _transform.position };
        }
    }
}

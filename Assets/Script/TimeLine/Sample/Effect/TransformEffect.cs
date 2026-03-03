using System.Collections.Generic;
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
    public class PositionEffect : ITimeLineEffect<PositionFrame>
    {
        private Transform _transform;
        public PositionEffect(Transform transform)
        {
            _transform = transform;
        }
        public void ApplayFrame(PositionFrame preFrame, PositionFrame nextFrame, float timeAlpha)
        {
            _transform.position = Vector3.Lerp(preFrame.Position, nextFrame.Position, timeAlpha);
        }

        public void ApplayFrame(PositionFrame frame)
        {
            _transform.position = frame.Position;
        }
    }
    public class RotationEffect : ITimeLineEffect<RotationFrame>
    {
        private Transform _transform;
        public RotationEffect(Transform transform)
        {
            _transform = transform;
        }
        public void ApplayFrame(RotationFrame preFrame, RotationFrame nextFrame, float timeAlpha)
        {
            _transform.rotation = Quaternion.Lerp(preFrame.Rotation, nextFrame.Rotation, timeAlpha);
        }

        public void ApplayFrame(RotationFrame frame)
        {
            _transform.rotation = frame.Rotation;
        }
    }
}

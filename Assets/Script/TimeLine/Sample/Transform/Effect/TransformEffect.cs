using System.Collections.Generic;
using UnityEngine;

namespace Space.TimelineFramework.Sample
{

    public class PositionEffect : ITimeLineEffect<PositionFrame>
    {
        private Transform _transform;
        public PositionEffect(Transform transform)
        {
            _transform = transform;
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
        public void ApplayFrame(RotationFrame frame)
        {
            _transform.rotation = frame.Rotation;
        }
    }
}

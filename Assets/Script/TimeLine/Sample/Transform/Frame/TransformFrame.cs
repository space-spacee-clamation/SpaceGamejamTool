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
}

using UnityEngine;

namespace Space.TimelineFramework.Sample
{
    /// <summary>
    /// 通过Unity的Update来驱动时间轴的更新
    /// 适合于需要和Unity的生命周期紧密结合的时间轴
    /// 例如动画时间轴，游戏事件时间轴等
    /// </summary>
    public class UnityUpdateTimeTick : MonoBehaviour, ITimeTick
    {
        private int currentTick = 0;
        private float time = 0f;
        public float TickScale { get; set; } = 1f;
        public int GetCurrentTick()
        {
            return currentTick;
        }
        public float GetTime()
        {
            return time;
        }
        public void ResetTick()
        {
            currentTick = 0;
            time = 0f;
        }
        public void ResetTickTo(int tick, float time)
        {
            currentTick = tick;
            this.time = time;
        }
        private int frameBuffer = 0;
        /// <summary>
        /// 通过Update来驱动时间轴的更新
        /// </summary>
        void Update()
        {
            time += Time.deltaTime * TickScale;
            frameBuffer++;
            if (frameBuffer >= 1 / TickScale)
            {
                currentTick++;
                frameBuffer = 0;
            }
        }
    }
}

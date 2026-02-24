using UnityEngine;

namespace Space.TimelineFramework.Sample
{
    [RequireComponent(typeof(UnityUpdateTimeTick))]
    public class AnimationTimeline : MonoBehaviour
    {
        [SerializeField] private UnityUpdateTimeTick _unityUpdateTimeTick;
        private TimeLine _timeLine;
        private Animator _animator;
        void Awake()
        {
            if (_unityUpdateTimeTick == null)
            {
                _unityUpdateTimeTick = GetComponent<UnityUpdateTimeTick>();
            }
            _animator = GetComponent<Animator>();
        }
        void Start()
        {
            _timeLine = new TimeLine(_unityUpdateTimeTick);
        }
    }
}

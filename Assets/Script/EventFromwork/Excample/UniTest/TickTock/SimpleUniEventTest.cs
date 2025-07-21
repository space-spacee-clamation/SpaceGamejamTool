using System;
using Space.EventFramework;
using Space.EventFramework.BaseEvent;
using UnityEngine;
using Random = UnityEngine.Random;
namespace Space.EventFramework.Test
{
    [RequireComponent(typeof(IEventComponent))]
    public class SimpleUniEventTest : MonoBehaviour
    {
        private int test_a;
        private IEventComponent _eventSubscribeComponent;
        private void Start()
        {
            _eventSubscribeComponent=GetComponent<IEventComponent>();
            _eventSubscribeComponent.Subscribe(
                (in TimerEvent data) => 
            {
                if(data.Sender==gameObject)
                {
                    for (int i = 0; i < 100; i++)
                    {
                        test_a = i;
                    }
                }
            });
            _eventSubscribeComponent.Subscribe(
                (in GameObjectDestroyedEvent data) =>
            {
                if (data.ObjectInstance==gameObject)
                {
                    Debug.Log(data.ObjectInstance.name+" :  ______________awsl___________");

                }
            });
        }
        private float timer=0;
        private void Update()
        {
            if (timer<=0)
            {
                _eventSubscribeComponent.Publish(new TimerEvent(gameObject));
                timer=Random.Range(1.0f,1.5f);
            }
            timer-=Time.deltaTime;
        }
    }
    public struct TimerEvent : IEventData
    {
        public GameObject Sender;
        public DateTime Timer;
        public TimerEvent(GameObject sender)
        {
            Timer = DateTime.Now;
            Sender = sender;
        }
    }
}

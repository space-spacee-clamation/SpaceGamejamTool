using System;
using Space.EventFramework.BaseEvent;
using Space.GlobalInterface.EventInterface;
using UnityEngine;
using Random = UnityEngine.Random;
namespace Script.EventFromwork.Excample.UniTest.Test
{
    [RequireComponent(typeof(ITypeEventComponent))]
    public class SimpleUniEventTest : MonoBehaviour
    {
        private int test_a;
        private ITypeEventComponent _typeEventSubscribeComponent;
        public string massage;
        private void Start()
        {
            _typeEventSubscribeComponent=GetComponent<ITypeEventComponent>();
            _typeEventSubscribeComponent.Subscribe(
                (in TimerEvent data) => 
            {
                if(data.Sender==gameObject)
                {
                    Debug.Log ($"{gameObject.name} 收到信息   来自 {data.Sender.name.ToString()} ");
                }
            });
            _typeEventSubscribeComponent.Subscribe(
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
                _typeEventSubscribeComponent.Publish(new TimerEvent(gameObject));
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

using System;
using System.Collections.Generic;
using Space.EventFramework;
using UnityEngine;
namespace Script.EventFromwork.Excample.UniTest.RedGreen
{
    [RequireComponent(typeof(MonoEventSubComponent))]
    public class Light : MonoBehaviour
    {
        private float timer=0.0f;
        private List<float> cds=new List<float>(){1f,2f};
        /// <summary>
        ///  index =0 为绿灯
        /// index =1 为红灯
        /// </summary>
        private int index = 0;
        private MonoEventSubComponent _monoEventSubComponent;
        private void Start()
        {
            _monoEventSubComponent=GetComponent<MonoEventSubComponent>();
        }
        private void Update()
        {
            timer-=Time.deltaTime;
            if (timer<0)
            {
                timer=cds[index];
                index=(index+1)%2;
                _monoEventSubComponent.Publish(new LightChangeEvent(index));
            }
        }
    }
    public struct LightChangeEvent : IEventData
    {
        /// <summary>
        ///  index =0 为绿灯
        /// index =1 为红灯
        /// </summary>
        public int index;
        public LightChangeEvent(int index)
        {
            this.index=index;
        }
    }
}
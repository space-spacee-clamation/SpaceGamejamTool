using Space.EventFramework;
using UnityEngine;
namespace Script.EventFromwork.Excample.UniTest.RedGreen
{
    [RequireComponent(typeof(MonoEventSubComponent))]
    public class Car : MonoBehaviour
    {
        public void Start()
        {
            GetComponent<MonoEventSubComponent>().Subscribe<LightChangeEvent>(OnChangeColor);
        }
        private void OnChangeColor(in LightChangeEvent data)
        {
            if (data.index==0)
            {
                Debug.Log($"我是汽车:{gameObject.name}  现在是绿灯");
            }
            if (data.index==1)
            {
                Debug.Log($"我是汽车:{gameObject.name}  现在是红灯");
            }
        }
    }
}
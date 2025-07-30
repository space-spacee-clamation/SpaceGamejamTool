using System;
using System.Collections.Generic;
using Space.GlobalInterface.Lifecycle;
using Space.GlobalInterface.PipelineInterface;
using Unity.VisualScripting;
using UnityEngine;
namespace Space.LifeControllerFramework.PipelineLifeController.PipelineComponent
{
    /// <summary>
    /// 计算器管道
    /// 只有在时间到的时候会触发
    /// </summary>
    public class TimerPipe<T> : ALifePipelineComponent<TimerPipe<T>> where T : ILifecycleSubscriber
    {
        private Action<T,LifecyclePipelineManager.LifecyclePipelineContext> handeler = null;
        private float FixedTime;
        float timerRem = 0;
        /// <param name="parameters">
        /// 初始化参数
        /// (int)优先级  (string)名字 (float)FixedTime ( Action<T,LifecyclePipelineContext>) 合规的接口处理器
        /// </param>
        public override IPipelineStage<LifecyclePipelineManager.LifecyclePipelineContext> SetParams(params object[] parameters)
        {
            FixedTime =(float) parameters[2] ;
           if(FixedTime<=0) throw new Exception("FixedTime is 0");
            handeler = (Action<T,LifecyclePipelineManager.LifecyclePipelineContext>)parameters[3];
            return base.SetParams(parameters);
        }
        public override void Execute(LifecyclePipelineManager.LifecyclePipelineContext context)
        {
            timerRem-=context.UpdateContext.DeltaTime;
            while (timerRem<0)
            {
                List<ILifecycleSubscriber> resList= context.GetSubscribers(PhaseName);
                foreach (var item in resList)
                {
                    if(item is T timer)handeler.Invoke(timer,context);
                    else
                    {
                        Debug.Log($" <{item.GetType().FullName}> 尝试被注册进入 TimerPipe ,但是其没有实现 {typeof(T)} 接口");
                        context.UnSubscribe(PhaseName, item);
                    }
                }
                //不直接置0追回帧
                timerRem+=FixedTime;
            }
        }
    }
}
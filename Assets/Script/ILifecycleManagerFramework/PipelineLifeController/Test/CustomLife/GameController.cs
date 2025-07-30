using System;
using Space.GlobalInterface.Lifecycle;
using Space.LifeControllerFramework.PipelineLifeController;
using Space.LifeControllerFramework.PipelineLifeController.PipelineComponent;
using UnityEngine;
namespace Space.ILifecycleManagerFramework.PipelineLifeController.Test.CustomLife
{
    public class GameController : MonoBehaviour
    {
        private LifecyclePipelineManager lifecyclePipelineManager;
        private void Awake()
        {
            lifecyclePipelineManager = new LifecyclePipelineManager();
            //TODO: 可以实现一个类似适配器的东西   目前反正是硬编码无所谓了
            lifecyclePipelineManager.AddPhase(new OncePipe<IGameStart>().
                SetParams(
                    new OncePipe<IGameStart>.
                    CreatOncePipeInfo(
                        -2,
                        TestCustomPhase.START,
                        (start, context) => {
                            start.Invoke();
                        }
                        )) as ILifecyclePhase);
            //TODO: 更替创建方式
            lifecyclePipelineManager.AddPhase(new UpdatePipe<IGameAnimation>().SetParams(51,TestCustomPhase.ANIMATION) as ILifecyclePhase);
            lifecyclePipelineManager.AddPhase(new UpdatePipe<IGameWorldUpdate>().SetParams(52,TestCustomPhase.WORLD) as ILifecyclePhase);
            lifecyclePipelineManager.AddPhase(new TimerPipe<IGamePhysics>().SetParams(53,TestCustomPhase.PHYSICS,0.05f) as ILifecyclePhase);
            lifecyclePipelineManager.AddPhase(new UpdatePipe<IGameBOOOOOM>().SetParams(4,TestCustomPhase.BOOOOOM) as ILifecyclePhase);
        }
    }
}
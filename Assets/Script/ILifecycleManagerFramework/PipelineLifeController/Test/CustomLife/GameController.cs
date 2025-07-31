using Space.GlobalInterface.Lifecycle;
using Space.LifeControllerFramework.PipelineLifeController;
using Space.LifeControllerFramework.PipelineLifeController.PipelineComponent;
using UnityEngine;
namespace Space.ILifecycleManagerFramework.PipelineLifeController.Test.CustomLife
{
    public class GameController : MonoBehaviour
    {
        public static LifecyclePipelineManager Instance;
        private LifecyclePipelineManager lifecyclePipelineManager;
        private void Awake()
        {
            lifecyclePipelineManager = new LifecyclePipelineManager();
            Instance = lifecyclePipelineManager;
            //TODO: 可以实现一个类似适配器的东西   目前反正是硬编码无所谓了
            lifecyclePipelineManager.AddPhase(new OncePipe<IGameStart>().SetParams(
                    new OncePipe<IGameStart>.PipeCreatInfo(
                        -2,
                        TestCustomPhase.START,
                        (start, context) => {
                            start.Invoke();
                        }
                    ))
                as ILifecyclePhase);
            //TODO: 更替创建方式
            lifecyclePipelineManager.AddPhase(new UpdatePipe<IGameAnimation>().SetParams(
                    new UpdatePipe<IGameAnimation>.PipeCreatInfo(
                        51,
                        TestCustomPhase.ANIMATION,
                        (a, context) => {
                            a.AniUpdate(context.UpdateContext);
                        }))
                as ILifecyclePhase);
            lifecyclePipelineManager.AddPhase(new UpdatePipe<IGameWorldUpdate>().SetParams(new UpdatePipe<IGameWorldUpdate>.PipeCreatInfo(
                52,
                TestCustomPhase.WORLD,
                (a, c) => a.WorldUpdate(c.UpdateContext))) as ILifecyclePhase);
            lifecyclePipelineManager.AddPhase(new TimerPipe<IGamePhysics>().SetParams(new TimerPipe<IGamePhysics>.PipeCreatInfo(
                54, TestCustomPhase.PHYSICS,
                (a, c, t) =>
                    a.PhysicsUpdate(c.UpdateContext, t))) as ILifecyclePhase);
            lifecyclePipelineManager.AddPhase(new UpdatePipe<IGameBOOOOOM>().SetParams(new UpdatePipe<IGameBOOOOOM>.PipeCreatInfo(
                5, TestCustomPhase.BOOOOOM,
                (a, c) => a.Boooom())) as ILifecyclePhase);
        }
    }
}
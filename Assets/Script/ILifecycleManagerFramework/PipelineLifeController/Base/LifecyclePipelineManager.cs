using System.Collections.Generic;
using System.Linq;
using Space.GlobalInterface.Lifecycle;
using Space.GlobalInterface.PipelineInterface;
namespace Space.LifeControllerFramework.PipelineLifeController
{
    public class LifecyclePipelineManager : ILifecycleManager
    {
        private LifecyclePipeline pipeline;
        public class LifecyclePipelineContext : IPipelineContext
        {
            /// <summary>
            /// 注册的subscribers
            /// 还有其对应注册的阶段
            /// </summary>
            Dictionary<string,List<ILifecycleSubscriber>> subscribers;
            /// <summary>
            /// 置入IGameUpdate的参数
            /// 直接由外部传入，不进行管理
            /// </summary>
            public ILifecycleManager.UpdateContext UpdateContext;

            public LifecyclePipelineContext()
            {
                subscribers = new Dictionary<string, List<ILifecycleSubscriber>>();
            }
            public void Subscriber(string name,ILifecycleSubscriber subscriber)
            {
                if (subscribers.TryGetValue(name, out List<ILifecycleSubscriber> subscribersList))
                {
                    subscribersList.Add(subscriber);
                    return;
                }
                subscribers.Add(name, new List<ILifecycleSubscriber>() { subscriber });
            }
            public void UnSubscribe(string name,ILifecycleSubscriber subscriber)
            {
                if (subscribers.TryGetValue(name, out List<ILifecycleSubscriber> subscribersList))
                {
                    subscribersList.Remove(subscriber);
                    return;
                }
            }
            public void UnSubscribe(ILifecycleSubscriber subscriber)
            {
                var find = subscribers.Values.First(a => a.Contains(subscriber));
                if (find!=null)
                {
                    find?.Remove(subscriber);
                }
            }
            public List<ILifecycleSubscriber> GetSubscribers(string PhaseName)
            {
                return subscribers.ContainsKey(PhaseName) ? subscribers[PhaseName] : null;
            }
        }
        
        /// <summary>
        /// 生命周期管理上下文
        /// 负责存贮各种数据
        /// </summary>
        private LifecyclePipelineContext context;
        
        public void InitLifecycle()
        {
            pipeline=new LifecyclePipeline();
            context=new LifecyclePipelineContext();
        }
        public void Subscribe(string PhaseName, ILifecycleSubscriber lifeState)
        {
            context.Subscriber(PhaseName, lifeState);
        }
        public void Unsubscribe(string PhaseName, ILifecycleSubscriber lifeState)
        {
            context.UnSubscribe(PhaseName, lifeState);
        }
        public void Unsubscribe(ILifecycleSubscriber lifeState)
        {
            context.UnSubscribe(lifeState);
        }
        public void AddPhase(ILifecyclePhase lifePhase)
        {
            pipeline.AddStage(lifePhase as IPipelineStage<LifecyclePipelineContext>);
        }
        public void RemovePhase(ILifecyclePhase lifePhase)
        {
            pipeline.RemoveStage(lifePhase as IPipelineStage<LifecyclePipelineContext>);
        }
        public void Update(ILifecycleManager.UpdateContext context)
        {
            
        }

    }
}
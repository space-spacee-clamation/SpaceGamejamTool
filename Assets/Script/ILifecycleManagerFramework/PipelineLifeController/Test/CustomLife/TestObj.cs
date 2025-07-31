using System;
using Space.GlobalInterface.Lifecycle;
using Space.LifeControllerFramework.PipelineLifeController;
using UnityEngine;
namespace Space.ILifecycleManagerFramework.PipelineLifeController.Test.CustomLife
{
    public class TestObj : MonoBehaviour ,IGameAnimation,IGamePhysics,IGameStart,IGameBOOOOOM,IGameWorldUpdate
    {

        private void Start()
        {
            GameController.Instance.Subscribe(typeof(IGameAnimation).FullName, this);
            GameController.Instance.Subscribe(typeof(IGamePhysics).FullName, this);
            GameController.Instance.Subscribe(typeof(IGameStart).FullName, this);
            GameController.Instance.Subscribe(typeof(IGameBOOOOOM).FullName, this);
            GameController.Instance.Subscribe(typeof(IGameWorldUpdate).FullName, this);

        }
        public void AniUpdate(in ILifecycleManager.UpdateContext context)
        {
            Debug.Log($"{gameObject.name} :: AniUpdate DeltaTime  {context.DeltaTime}");
        }
        public void PhysicsUpdate(in ILifecycleManager.UpdateContext context, float deltaTime)
        {
            
        }
        public void Invoke()
        {
            
        }
        public void Boooom()
        {
            
        }
        public void WorldUpdate(in ILifecycleManager.UpdateContext context)
        {
            
        }
    }
}
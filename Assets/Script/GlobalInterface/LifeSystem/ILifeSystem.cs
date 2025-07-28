using Space.GlobalInterface.PipelineInterface;
namespace Space.GlobalInterface.LifeSystem
{
    public interface ILifeControllerSystem
    {
        public class LifeControllerContext : IPipelineContext
        {
            /// <summary>
            /// DeltaTime时间 (受缩放影响)
            /// </summary>
            public float DeltaTime;
            /// <summary>
            /// DeltaTime时间 (不受缩放影响） 
            /// </summary>
            public float UnscaledDeltaTime;
            /// <summary>
            /// 总帧数
            /// </summary>
            public float FrameCount;
            /// <summary>
            /// 游戏运行时间(受timescale影响)
            /// </summary>
            public float GameTime;
            /// <summary>
            /// 真实运行时间 不收缩放影响
            /// </summary>
            public float RealtimeSinceStartup;
            
            //TODO:更多的需要的参数
        }
        //TODO: 生命周期注册，生命周期管线(较为固定可以不使用factory生成)，生命周期
        public void InitLifeSystem();
        /// <summary>
        /// 更具提供的上下文进行框架生命周期的Update
        /// </summary>
        public void Update(LifeControllerContext lifeControllerContext);
    }
}
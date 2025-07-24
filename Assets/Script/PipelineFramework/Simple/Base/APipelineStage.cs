using Space.GlobalInterface.PipelineInterface;
namespace Space.PipelineFramework.Simple
{
    public abstract class APipelineStage<T> :  IPipelineStage where T : APipelineStage<T>,new()
    {
        public virtual int DefaultPriority { get; protected set; }
        /// <summary>
        /// 这套框架里其实没怎么用，毕竟用反射了qwq ,所以说其实无所谓的东西😋
        /// </summary>
        public virtual string StageName => typeof(T).Name;
        public virtual IPipelineStage SetParams(params object[] parameters)
        {
            return this;
        }
        public abstract void Execute(IPipelineContext context);
        public IPipelineStage Clone()
        {
            return new T();
        }
    }
}
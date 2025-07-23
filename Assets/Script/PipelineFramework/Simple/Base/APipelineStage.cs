using Space.GlobalInterface.PipelineInterface;
namespace Space.PipelineFramework.Simple
{
    public abstract class APipelineStage<T> :  IPipelineStage where T : APipelineStage<T>,new()
    {
        public virtual int Priority =>0;
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
using Space.GlobalInterface.PipelineInterface;
namespace Space.PipelineFramework
{
    public static class GlobalPipelineStageFactory
    {
        public static IPipelineFactory Instance = FrameworkFactory.GetInstance<IPipelineFactory>();
    }
}
namespace Space.TimelineFramework
{
    /// <summary>
    /// 此处的frame是抽象的概念而不是具体的帧，frame是timeline的最小单位
    /// frame应该是一些单纯数据类
    /// 通过source进行生成 同时通过source进行写入
    /// 生命周期由Track进行管理
    /// </summary>
    public interface ITimeLineFrame
    {

    }
}

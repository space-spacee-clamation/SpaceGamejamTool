[//]: # (# 简易的管道系统)

[//]: # (使用管道的框架去处理一些数据流处理一类的工作，之所以称之为简单，是因为此版的管道组装使用的是硬编码，实现模块的管道也是通过继承接口和在其构造函数里面构建（参考DamagePipeLine）。)

[//]: # (实际上管道的组装和构建都可以通过使用字符串反序列化，这样可以给编辑器和策划配表提供便利。)

[//]: # (## 目录结构)

[//]: # ()
[//]: # ( - Base 简易管道架构的基础脚本)

[//]: # (   - APipelineStage 抽象的管道组件的基类，实际上只是代替实现了一个克隆效果，克隆是为了工厂使用享元模式创造实例)

[//]: # (   - Pipeline 管道，负责把Stage组装起来)

[//]: # (   - PipelineContext 管道上下文，实现IPipelineContext接口)

[//]: # (   - Factory 这个代码实现了两种工厂，一个依赖特征加反射，一个依赖配置字段，实际上都是配置字段罢了&#40;&#41;，如果需要更换工厂，只需要更换FramworkFactory中对应的实例即可，这两个工厂也体现了该框架的可替换性)

[//]: # (     - ReflectionPipeLineFactory 依照反射实现的管道工厂)

[//]: # (     - DictionaryPipelineFactory 依赖配置字段实现工厂，字段格式是&#40;"索引的key","反射使用的全名"&#41;)

[//]: # ( - Example)

[//]: # (   - TestPipeline 一个纯测试的管道，无任何实际意义)

[//]: # (   - DamagePipeLine 一个伤害计算管道的例子)
//TODO: 修改readme.md
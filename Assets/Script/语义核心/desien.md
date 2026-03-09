语义核心是什么（要解决的问题）
语义核心的目标是把“教程内容”从“具体实现细节”里解耦出来：内容只引用稳定的语义 ID（例如某个 UI 元素、某个游戏事件、某个站台区域），运行时再把这些 ID 解析成真实对象/句柄（RectTransform、世界坐标、事件流等）。这样可以在不重构外部系统的情况下，把教程系统接进来，并且让内容配置在版本迭代中更稳定。

设计思路（原则与取舍）
语义优先，解析后置
内容侧只写 UIAnchorId / GameEventId / WorldAnchorId 这类“名字”，运行时用 Registry/Resolver 把名字映射成可用句柄。

反腐层（Anti-Corruption Layer）隔离外部系统
外部系统（HUD、Mission、Replay、TrainBase 等）不需要理解教程内部；教程也不直接依赖外部系统内部类型与状态机细节。
用 SignalHub/Bridge 把外部信号“翻译”为教程可消费的统一信号。

可诊断性优先（Diagnostics First）
解析失败、触发失败、准入失败都要能给出“为什么”，并能在 Debug UI / 报表里定位到具体 ID、具体 Step。

MVP 可落地、可增量扩展
先用最小的 ID 集合 + 最小的 Step/Trigger/Rule 覆盖验证案例；后续新增类型用扩展点（SerializeReference + StepFactory + Resolver）扩展，不强迫全局重构。

语义体系的关键点（“有点”）
稳定引用（Stable Reference）
语义 ID 作为“内容契约”，避免直接引用对象实例或场景路径，降低改 UI 层级/Prefab 时的破坏性。

双层语义：语义区域 vs 精确位置
WorldAnchorId 用于“语义区域/点位”（例如站台范围、停车目标点附近区域）。
精确到轨道里程/区间用 TrackLocationRef / TrackRangeRef，避免把 WorldAnchorId 设计成万能坐标系统导致内容与轨道实现强耦合。

统一信号流
触发（Trigger）与步骤等待（WaitGameEvent）都通过统一的 TutorialTriggerSignal / TutorialGameEvent 来表达，教程不关心信号来自 Mission、Train、Replay 还是别的系统。

内容编排 vs 执行逻辑分离
ScriptableObject 负责“编排”，运行时 Step 实例负责“执行”，使得内容迭代不需要动运行时代码结构。

关键类的拆分（核心职责划分）
1) 语义 ID 与解析
UIAnchorRegistry + UIAnchorProvider
职责：把 UIAnchorId -> IUIAnchorHandle（例如 RectTransform 句柄）建立映射、维护生命周期、提供可诊断的 Resolve。
WorldAnchorRegistry / TrackResolver（设计层）
职责：把 WorldAnchorId / TrackLocationRef 解析为世界位置/范围句柄；对轨道实现细节做隔离。
2) 信号桥接（反腐层）
TutorialSignalHub
职责：从现有系统订阅事件/状态变化（Mission state、列车档位、回放结束、冲标、门循环等），发布成教程统一信号：
TutorialTriggerSignal（用于“能否启动教程”的触发）
TutorialGameEvent（用于“步骤等待/重启条件”的事件）
价值：外部系统不改结构，教程也不被外部类型污染。
3) 内容层（ScriptableObject 编排）
TutorialSettingsSO：入口配置（哪些 ContentCollection Addressables 要加载）
TutorialContentCollectionSO：内容包（CollectionId/Version + Tutorials 列表）
TutorialDefinitionSO：单个教程定义（Triggers、Eligibility、Steps、RestartOnGameEventIds 等）
StepConfigBase / TriggerConfigBase / EligibilityRuleBase（SerializeReference 多态）
价值：类型可扩展，避免“小功能一个 SO”的资产爆炸。
4) 运行时执行层
TutorialKernel（系统层 orchestrator）
职责：加载内容、监听触发、做准入/互斥、启动/重启 runner、记录进度与埋点、做必要桥接（例如 Mission 结果 -> GameEvent）。
TutorialSession
职责：一次运行的上下文（状态、黑板、外部服务句柄、token/取消等）。
TutorialRunner
职责：按顺序驱动 Step 执行（Start/Cancel/Pause/Resume/SkipCurrent），保证幂等与可控。
StepFactory + ITutorialStep
职责：把 StepConfigBase 实例化成可执行 Step（ShowHint、HighlightUIAnchor、WaitGameEvent、WaitCondition…）。
5) 可观测与可制作性（Editor/Debug）
TutorialDebugView：运行时调试入口（ForceStart/Skip/Dump Anchors）
TutorialAssetGeneratorWindow / TutorialAuthoringWindow：编辑器侧资产生成、审计、可视化编辑与校验
核心调用链（从“语义”到“可执行”）
启动链路：外部系统变化 → SignalHub 发布 TutorialTriggerSignal → Kernel 选教程并做准入 → 创建 Session → Runner 执行 Steps
解析链路：Step 中引用 UIAnchorId / WorldAnchorRef / GameEventId → Registry/Resolver/Subscriber 解析成真实句柄或订阅 → Step 产生完成/失败结果
诊断链路：解析失败/条件不满足 → 失败原因结构化记录（含 ID/StepId）→ DebugView/Editor 校验列表可定位
总结一句话
语义核心的本质是：用稳定语义 ID 作为内容契约 + 用 Registry/Resolver/SignalHub 把契约翻译成运行时真实对象与信号，并用系统层（Kernel/Runner/Progress/Telemetry）把“何时启动、能否执行、如何诊断”统一收口，从而实现低耦合、可增量接入、可长期维护的教程体系。

1) 先给你一个“教程影响游戏逻辑”的总框架
你现在已经能理解“外部事件/功能触发教程”，那反向的“教程影响游戏”通常分三层（从安全到强力）：

A. 纯表现层影响（推荐默认）：不改任何业务真值，只在画面上“提示/高亮/描边/箭头/遮罩”。
例子：UI 描边、显示提示文本、在屏幕上画框。
B. 输入/交互层影响（次推荐）：不直接改业务真值，但可以“限制输入”“聚焦交互”“拦截某些按键”，让玩家按教程引导操作。
例子：暂停某些输入通道、锁定镜头/焦点、要求先点某按钮才能继续。
C. 业务真值层影响（谨慎）：直接修改列车/任务/状态机等真值，风险最大、最容易耦合。
一般只在“测试/Debug”或“非常明确且已有命令链路”的情况下做（例如通过既有 Command/Intent 链路而不是直接改字段）。
你问的“UI 描边”，属于 A 层：教程只输出“我想高亮哪个屏幕区域”，真正的“怎么画出来”由 OverlayView 负责。

2) UI 描边（高亮）的真实链路：Step → Anchor 解析 → OverlayService → OverlayView
2.1 Step：决定“高亮哪个 UIAnchorId”
在 StepFactory 里，HighlightUIAnchorStep 会在进入步骤时解析 UIAnchor，然后把一个“屏幕 Rect 提供函数”注册给 Overlay。

关键逻辑在这里：

public void Enter(TutorialSession session)
{
    // ...
    if (!session.UIAnchors.TryResolveVerbose(_config.UIAnchorId, out var handle, out var failure))
    {
        // resolve failed -> skip/abort/or show hint then continue
        // ...
        return;
    }
    _highlightHandle = session.Overlay.HighlightRect(this, () =>
    {
        if (handle == null || !handle.IsValid)
        {
            return null;
        }
        return handle.TryGetScreenRect(out var r) ? r : null;
    });
    session.AddSubscription(_highlightHandle);
}
这里的重点是：教程不会直接画描边，它只注册一个 Func<Rect?>，告诉系统“你需要的时候可以来问我当前的屏幕矩形”。

2.2 UIAnchorRegistry：把语义 ID 解析成可测量的 UI 句柄
UIAnchorRegistry 维护 anchorId -> IUIAnchorHandle 的映射，RectTransformUIAnchorHandle 负责把 RectTransform 转成屏幕 Rect：

public bool TryGetScreenRect(out Rect rect)
{
    // ...
    var canvas = _target.GetComponentInParent<Canvas>();
    var cam = canvas != null && canvas.renderMode != RenderMode.ScreenSpaceOverlay ? canvas.worldCamera : null;
    var corners = new Vector3[4];
    _target.GetWorldCorners(corners);
    // convert world corners -> screen points
    // rect = Rect.MinMaxRect(min.x, min.y, max.x, max.y);
    return true;
}
也就是说：语义 ID（ui.xxx）先解析到 RectTransform，再被转换成屏幕坐标矩形，后续绘制完全不需要知道 UI 层级细节。

2.3 OverlayService：保存“提示文字”和“高亮矩形提供者”
OverlayService 是一个非常轻量的“状态聚合器”，它不画，只存：

private readonly Dictionary<object, string> _hints = new();
private readonly Dictionary<object, Func<Rect?>> _highlights = new();
public IDisposable HighlightRect(object owner, Func<Rect?> screenRectProvider)
{
    _highlights[owner] = screenRectProvider;
    return new Handle(() => _highlights.Remove(owner));
}
这也解释了为什么它返回 IDisposable：Step 结束时 Dispose() 就会自动取消高亮，不会泄漏状态。

2.4 OverlayView：真正“画描边”的地方
最终绘制发生在 TutorialOverlayView.OnGUI()：读取 OverlayService 的 rect 列表，然后用 GUI.DrawTexture 画四条边。

_overlay.GetHighlightRects(_rects);
GUI.color = new Color(1f, 0.85f, 0.1f, 0.55f);
for (int i = 0; i < _rects.Count; i++)
{
    DrawRectBorder(_rects[i], 3);
}
所以你可以把“描边实现”理解为：

教程侧：只声明“高亮这个语义目标”（UIAnchorId）
解析侧：把语义目标变成“屏幕矩形”
表现侧：统一画出来（描边/遮罩/箭头都可以在这里扩展）
3) 回到你的核心疑问：教程如何“影响游戏逻辑”？
用你现在的架构来看，教程对游戏的影响应优先走这条路线：

教程影响“玩家感知与操作路径”（提示/高亮/限制输入）
而不是“教程直接改业务真值”。
“UI 描边”就是最典型的：它影响的是玩家的注意力与操作目标，不改变业务状态，但能显著改变玩家行为，从而间接推动业务逻辑进入某个状态（例如玩家点了按钮、拉了制动、完成了靠站）。

如果你接下来想做更强的影响（例如“站内禁止加速”的强约束），建议优先做 B 层输入/仲裁：在输入链路或安全联锁层提供一个“Tutorial Gate/Constraint”，教程只开关 gate，而不是直接把 PowerNotch 改回 0。这样耦合最小、也更安全。

4) 你可以继续问我两种你最关心的“影响方式”
为了更快把你要的那部分梳理清楚，你可以直接说你更关注哪类：

(1) 纯表现扩展：比如遮罩挖洞、箭头指引、动画描边、世界物体高亮
(2) 输入/控制扩展：比如禁用某些键、锁定某 UI 焦点、暂停/恢复、强制按顺序操作

using System.Collections.Generic;
using Space.GlobalInterface.PipelineInterface;
namespace Space.PipelineFramework.Simple.Example.DamagePipeLine
{
    /// <summary>
    /// 添加火焰修正(玩家身上着火了(?))
    /// 也可也是从技能上拿到属性
    /// 看自己设计了
    /// </summary>
    public class ApplyFireDamageTypeStep : APipelineStage<BaseDamageCalculate>
    {
        public override void Execute(IPipelineContext context)
        {
            int damage= context.GetSharedData<int>("Damage");
            damage += 1;
            context.SetSharedData("FireDamage",1);
            context.SetSharedData("Damage", damage);
            if (context.TryGetSharedData<List<string>>("DamageTag", out List<string> list))
            {
                list.Add("Fire");
            }
            else
            {
                context.SetSharedData("DamageTag", new List<string>(){"Fire"});
            }
        }
    }
}
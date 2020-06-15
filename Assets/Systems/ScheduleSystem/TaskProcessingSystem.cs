using System.Collections.Generic;
using Assets.Core;
using Entitas;
using System.Linq;

public partial class TaskProcessingSystem : OnDateChange
{
    public TaskProcessingSystem(Contexts contexts) : base(contexts) {}

    protected override void Execute(List<GameEntity> entities)
    {
        GameEntity[] tasks = Cooldowns.GetTimedActions(gameContext);
        var date = ScheduleUtils.GetCurrentDate(gameContext);

        for (var i = tasks.Length - 1; i >= 0; i--)
        {
            var t = tasks[i];
            var task = t.timedAction;

            var EndTime = task.EndTime;

            if (t.isTask)
            {
                if (date >= EndTime && !task.isCompleted)
                {
                    Cooldowns.ProcessTask(task, gameContext);
                    t.timedAction.isCompleted = true;
                }
            }


            // 
            if (date > EndTime)
                t.Destroy();
        }

        var products = Companies.GetProductCompanies(gameContext);

        foreach (var p in products)
        {
            foreach (var t in p.team.Teams)
            {
                foreach (var task in t.Tasks)
                {
                    if (task is TeamTaskFeatureUpgrade)
                    {
                        var upgrade = (task as TeamTaskFeatureUpgrade);

                        if (!Products.IsUpgradingFeature(p, gameContext, upgrade.NewProductFeature.Name))
                        {
                            Products.UpgradeFeature(p, upgrade.NewProductFeature.Name, gameContext);
                        }
                    }
                }
            }
        }
    }




    protected override bool Filter(GameEntity entity) => entity.hasDate;

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) => context.CreateCollector(GameMatcher.Date);
}

using System.Collections.Generic;
using Assets.Utils;
using Entitas;

public class TaskProcessingSystem : OnDateChange
{
    public TaskProcessingSystem(Contexts contexts) : base(contexts)
    {
    }

    protected override void Execute(List<GameEntity> entities)
    {
        GameEntity[] tasks = contexts.game.GetEntities(GameMatcher.Task);

        for (var i = tasks.Length - 1; i > 0; i++)
        {
            var t = tasks[i];
            if (t.task.isCompleted)
            {
                ProcessTask(t.task);

                t.Destroy();
            }
        }
    }

    void AcquireCompany(CompanyTask task)
    {
        //var nicheType = (task as CompanyTaskExploreMarket).NicheType;

        //var niche = NicheUtils.GetNicheEntity(gameContext, nicheType);
        //niche.AddResearch(1);
    }

    void ExploreMarket(CompanyTask task)
    {
        var nicheType = (task as CompanyTaskExploreMarket).NicheType;

        var niche = NicheUtils.GetNicheEntity(gameContext, nicheType);
        niche.AddResearch(1);
    }

    private void ProcessTask(TaskComponent taskComponent)
    {
        var task = taskComponent.CompanyTask;
        switch (task.CompanyTaskType)
        {
            case CompanyTaskType.ExploreMarket: ExploreMarket(task); break;
            case CompanyTaskType.AcquiringCompany: AcquireCompany(task); break;
        }
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasDate;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Date);
    }
}

using System.Collections.Generic;
using Assets.Utils;
using Entitas;
using UnityEngine;

public partial class TaskProcessingSystem : OnDateChange
{
    public TaskProcessingSystem(Contexts contexts) : base(contexts)
    {
    }

    protected override void Execute(List<GameEntity> entities)
    {
        GameEntity[] tasks = ScheduleUtils.GetTasks(gameContext);
        var date = ScheduleUtils.GetCurrentDate(gameContext);

        for (var i = tasks.Length - 1; i >= 0; i--)
        {
            var t = tasks[i];
            var task = t.task;

            var EndTime = task.EndTime;

            if (date >= EndTime && !task.isCompleted)
            {
                //Debug.Log("Finishing task " + task.CompanyTask.CompanyTaskType);

                ProcessTask(task);
                t.task.isCompleted = true;
            }

            if (date > EndTime + 30)
                t.Destroy();
        }
    }

    private void ProcessTask(TaskComponent taskComponent)
    {
        var task = taskComponent.CompanyTask;
        switch (task.CompanyTaskType)
        {
            case CompanyTaskType.ExploreMarket: ExploreMarket(task); break;
            case CompanyTaskType.ExploreCompany: ExploreCompany(task); break;
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

public partial class TaskProcessingSystem : OnDateChange
{
    void AcquireCompany(CompanyTask task)
    {
        //var nicheType = (task as CompanyTaskExploreMarket).NicheType;

        //var niche = NicheUtils.GetNicheEntity(gameContext, nicheType);
        //niche.AddResearch(1);
    }

    void ExploreMarket(CompanyTask task)
    {
        var nicheType = (task as CompanyTaskExploreMarket).NicheType;

        var niche = NicheUtils.GetNiche(gameContext, nicheType);
        niche.AddResearch(1);
    }

    void ExploreCompany(CompanyTask task)
    {
        var id = (task as CompanyTaskExploreCompany).CompanyId;

        var c = CompanyUtils.GetCompanyById(gameContext, id);
        c.AddResearch(1);
    }
}
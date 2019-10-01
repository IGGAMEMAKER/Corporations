using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Utils;
using Entitas;

public class ScheduleCooldownProcessingSystem : OnDateChange
{
    public ScheduleCooldownProcessingSystem(Contexts contexts) : base(contexts)
    {
    }

    void ProcessTasks(List<Cooldown> cooldowns, GameEntity company, int date)
    {
        cooldowns.RemoveAll(c => date >= c.EndDate);

        company.ReplaceCooldowns(cooldowns);
    }

    protected override void Execute(List<GameEntity> entities)
    {
        GameEntity[] cooldowns = contexts.game.GetEntities(GameMatcher.Cooldowns);
        var container = CooldownUtils.GetCooldowns(contexts.game);

        var date = entities[0].date.Date;
        // old cooldown system
        foreach (var c in cooldowns)
            ProcessTasks(c.cooldowns.Cooldowns, c, date);

        // new cooldown system
        var removables = new List<string>();
        foreach (var c in container)
        {
            if (date >= c.Value.EndDate)
                removables.Add(c.Key);
        }

        foreach (var c in removables)
            container.Remove(c);
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
        var nicheType = (task as CompanyTaskExploreMarket).NicheType;

        var niche = NicheUtils.GetNicheEntity(gameContext, nicheType);
        niche.AddResearch(1);
    }

    void ExploreMarket(CompanyTask task)
    {
        var nicheType = (task as CompanyTaskExploreMarket).NicheType;

        var niche = NicheUtils.GetNicheEntity(gameContext, nicheType);
        niche.AddResearch(1);
    }

    private void ProcessTask(TaskComponent taskComponent)
    {
        var task = taskComponent.TaskType;
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

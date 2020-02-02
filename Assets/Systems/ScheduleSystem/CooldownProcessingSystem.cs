using System.Collections.Generic;
using Assets.Core;
using Entitas;

public class CooldownProcessingSystem : OnDateChange
{
    public CooldownProcessingSystem(Contexts contexts) : base(contexts) {}

    protected override void Execute(List<GameEntity> entities)
    {
        GameEntity[] cooldowns = contexts.game.GetEntities(GameMatcher.Cooldowns);
        var date = ScheduleUtils.GetCurrentDate(gameContext);
        //var date = entities[0].date.Date;

        var container = Cooldowns.GetCooldowns(contexts.game);

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

    void ProcessTasks(List<Cooldown> cooldowns, GameEntity company, int date)
    {
        cooldowns.RemoveAll(c => date >= c.EndDate);

        company.ReplaceCooldowns(cooldowns);
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

using System.Collections.Generic;
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

        foreach (var c in cooldowns)
            ProcessTasks(c.cooldowns.Cooldowns, c, entities[0].date.Date);
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

using System.Collections.Generic;
using Entitas;

public class ScheduleCooldownProcessingSystem : OnDateChange
{
    public ScheduleCooldownProcessingSystem(Contexts contexts) : base(contexts)
    {
    }

    void ProcessTasks(Dictionary<CooldownType, Cooldown> cooldowns, GameEntity c, int date)
    {
        List<CooldownType> toRemove = new List<CooldownType>();

        foreach (var val in cooldowns)
        {
            if (date >= val.Value.EndDate)
                toRemove.Add(val.Key);
        }

        foreach (var t in toRemove)
            cooldowns.Remove(t);
            
        c.ReplaceCooldowns(cooldowns);
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

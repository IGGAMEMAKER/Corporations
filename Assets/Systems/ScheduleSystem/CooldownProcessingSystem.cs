using System.Collections.Generic;
using Assets.Core;
using Entitas;

public partial class CooldownProcessingSystem : OnDateChange
{
    public CooldownProcessingSystem(Contexts contexts) : base(contexts) { }

    protected override void Execute(List<GameEntity> entities)
    {
        var container = Cooldowns.GetSimpleCooldownContainer(gameContext);
        var date = ScheduleUtils.GetCurrentDate(gameContext);

        var cooldowns = container.simpleCooldownContainer.Cooldowns;

        string[] taskNames = new string[cooldowns.Keys.Count];
        cooldowns.Keys.CopyTo(taskNames, 0);

        for (var i = taskNames.Length - 1; i >= 0; i--)
        {
            var cooldownName = taskNames[i];
            var simpleCooldown = cooldowns[cooldownName];

            var EndTime = simpleCooldown.EndDate;

            if (date >= EndTime)
                cooldowns.Remove(cooldownName);
        }
    }

    protected override bool Filter(GameEntity entity) => entity.hasDate;

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) => context.CreateCollector(GameMatcher.Date);
}

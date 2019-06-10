using System.Collections.Generic;
using Assets.Utils;
using Entitas;
using UnityEngine;

public class ScheduleCooldownProcessingSystem : OnDateChange
{
    public ScheduleCooldownProcessingSystem(Contexts contexts) : base(contexts)
    {
    }

    void ProcessTasks(List<Cooldown> cooldowns, GameEntity company, int date)
    {
        // TODO WILL BE SLOW
        cooldowns
            .FindAll(c => date >= c.EndDate)
            .ForEach(c => { ProcessCooldown(c, company); });

        cooldowns.RemoveAll(c => date >= c.EndDate);

        company.ReplaceCooldowns(cooldowns);
    }

    void ProcessCooldown(Cooldown cooldown, GameEntity company)
    {
        switch (cooldown.CooldownType)
        {
            case CooldownType.StealIdeas: ProcessStealCooldown(cooldown, company); break;
        }
    }

    void ProcessStealCooldown(Cooldown cooldown, GameEntity company)
    {
        var steal = (cooldown as CooldownStealIdeas);

        var targetId = steal.targetCompanyId;

        var target = CompanyUtils.GetCompanyById(gameContext, targetId);

        var leaderBonus = target.isTechnologyLeader ? 300 : 0;

        var amount = Random.Range(100 + leaderBonus, 200 + leaderBonus);

        CompanyUtils.AddResources(company, new Assets.Classes.TeamResource { ideaPoints = amount });
    }

    protected override void Execute(List<GameEntity> entities)
    {
        GameEntity[] cooldowns = contexts.game.GetEntities(GameMatcher.Cooldowns);

        Debug.Log("Cooldown processing system: " + cooldowns.Length);

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

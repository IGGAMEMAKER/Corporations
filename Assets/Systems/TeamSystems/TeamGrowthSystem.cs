using Assets.Core;
using Entitas;
using System.Collections.Generic;
using UnityEngine;

class UpdateTeamEfficiencySystem : OnMonthChange
{
    public UpdateTeamEfficiencySystem(Contexts contexts) : base(contexts) { }

    protected override void Execute(List<GameEntity> entities)
    {
        var companies = contexts.game.GetEntities(GameMatcher.AllOf(GameMatcher.Alive, GameMatcher.Company));

        foreach (var c in companies)
        {
            if (c.hasProduct)
            {
                Teams.UpdateTeamEfficiency(c, gameContext);
            }
        }
    }
}

class UpdatePlayerTeamEfficiencySystem : OnDateChange
{
    public UpdatePlayerTeamEfficiencySystem(Contexts contexts) : base(contexts) { }

    protected override void Execute(List<GameEntity> entities)
    {
        var playerFlagship = Companies.GetPlayerFlagship(gameContext);

        Teams.UpdateTeamEfficiency(playerFlagship, gameContext);
    }
}

class TeamGrowthSystem : OnMonthChange
{
    public TeamGrowthSystem(Contexts contexts) : base(contexts) {}

    protected override void Execute(List<GameEntity> entities)
    {
        var companies = contexts.game.GetEntities(GameMatcher.AllOf(GameMatcher.Alive, GameMatcher.Company));

        // maslov pyramid
        //
        // salary
        // interesting tasks
        // career ladder
        // feedback (i am doing useful stuff)
        // influence (become company shareholder)

        foreach (var c in companies)
        {
            var culture = Companies.GetActualCorporateCulture(c, gameContext);

            // gain expertise and recalculate loyalty
            foreach (var t in c.team.Teams)
            {
                foreach (var m in t.Managers)
                {
                    var human = Humans.GetHuman(gameContext, m);

                    // bigger the value... MORE chances to upgrade
                    var growth = Teams.GetManagerGrowthBonus(human, t, false, gameContext).Sum();

                    var willGrow = Random.Range(0, 100) < growth;

                    if (willGrow)
                    {
                        human.humanSkills.Roles[WorkerRole.CEO]++;

                        if (!human.hasHumanUpgradedSkills)
                            human.AddHumanUpgradedSkills(C.PERIOD - 1);
                    }
                }
            }
        }
    }
}

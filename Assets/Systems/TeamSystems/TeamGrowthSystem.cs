using Assets.Core;
using Entitas;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

class UpdateTeamEfficiencySystem : OnMonthChange
{
    public UpdateTeamEfficiencySystem(Contexts contexts) : base(contexts) { }

    protected override void Execute(List<GameEntity> entities)
    {
        var companies = Companies.GetProductCompanies(gameContext); // contexts.game.GetEntities(GameMatcher.AllOf(GameMatcher.Alive, GameMatcher.Company));

        foreach (var c in companies)
        {
            Teams.UpdateTeamEfficiency(c, gameContext);
        }
    }
}

class UpdatePlayerTeamEfficiencySystem : OnDateChange
{
    public UpdatePlayerTeamEfficiencySystem(Contexts contexts) : base(contexts) { }

    protected override void Execute(List<GameEntity> entities)
    {
        var playerFlagship = Companies.GetPlayerFlagship(gameContext);

        if (playerFlagship != null)
            Teams.UpdateTeamEfficiency(playerFlagship, gameContext);
    }
}

class TeamGrowthSystem : OnMonthChange
{
    public TeamGrowthSystem(Contexts contexts) : base(contexts) {}

    protected override void Execute(List<GameEntity> entities)
    {
        var companies = Companies.Get(gameContext); // contexts.game.GetEntities(GameMatcher.AllOf(GameMatcher.Alive, GameMatcher.Company));
        var humans = Humans.Get(gameContext);


        // maslov pyramid
        //
        // salary
        // interesting tasks
        // career ladder
        // feedback (i am doing useful stuff)
        // influence (become company shareholder)

        foreach (var c in companies)
        {
            //var culture = Companies.GetActualCorporateCulture(c, gameContext);

            // gain expertise and recalculate loyalty
            foreach (var t in c.team.Teams)
            {
                var managers = t.Managers.Select(m => humans.First(h => h.human.Id == m));

                foreach (var human in managers)
                {
                    bool hasTeacherInTeam = managers.Any(m1 => m1.humanSkills.Traits.Contains(Trait.Teacher) && m1.human.Id != human.human.Id);

                    // bigger the value... MORE chances to upgrade
                    var growth = Teams.GetManagerGrowthBonus(human, t, hasTeacherInTeam, gameContext).Sum();

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

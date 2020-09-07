using Assets.Core;
using Entitas;
using System.Collections.Generic;
using UnityEngine;

class WorkerHiringSystem : OnDateChange
{
    public WorkerHiringSystem(Contexts contexts) : base(contexts) { }

    protected override void Execute(List<GameEntity> entities) {
        // hiring workers

        var companies = contexts.game.GetEntities(GameMatcher.AllOf(GameMatcher.Alive, GameMatcher.Company));

        foreach (var c in companies)
        {
            foreach (var t in c.team.Teams)
            {
                if (t.Workers < 8)
                {
                    bool isUniversalTeam = Teams.IsUniversalTeam(t.TeamType);

                    var hiringSpeed = isUniversalTeam ? 25 : 15;
                    t.HiringProgress += hiringSpeed;

                    if (t.HiringProgress >= 100)
                    {
                        t.HiringProgress = 0;
                        t.Workers++;
                    }
                }
            }
        }
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

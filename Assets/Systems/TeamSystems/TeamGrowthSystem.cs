using Assets.Core;
using Entitas;
using System.Collections.Generic;
using UnityEngine;

class TeamGrowthSystem : OnPeriodChange
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

        var playerFlagshipId = Companies.GetPlayerFlagshipID(gameContext);

        foreach (var c in companies)
        {
            var culture = Companies.GetActualCorporateCulture(c, gameContext);

            // gain expertise and recalculate loyalty
            foreach (var m in c.team.Managers)
            {
                if (Random.Range(0, 100) < C.PERIOD)
                    continue;

                var humanId = m.Key;

                var human = Humans.GetHuman(gameContext, humanId);

                var growth = Teams.GetManagerGrowthBonus(human, gameContext).Sum();

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

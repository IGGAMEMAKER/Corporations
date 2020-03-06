using Assets.Core;
using Entitas;
using System;
using System.Collections.Generic;
using UnityEngine;

class MoraleManagementSystem : OnPeriodChange
{
    public MoraleManagementSystem(Contexts contexts) : base(contexts) {}

    protected override void Execute(List<GameEntity> entities)
    {
        var companies = contexts.game.GetEntities(GameMatcher.AllOf(GameMatcher.Alive, GameMatcher.Company));

        // pyramid
        //
        // salary
        // interest (anti routine)
        // career ladder
        // feedback (i am doing useful stuff)
        // influence (become company shareholder)

        foreach (var c in companies)
        {
            foreach (var m in c.team.Managers)
            {
                var humanId = m.Key;

                var human = Humans.GetHuman(gameContext, humanId);

                var relationship = human.humanCompanyRelationship;

                var newAdaptation   = Mathf.Clamp(relationship.Adapted + 4, 0, 100);
                var newLoyalty      = Mathf.Clamp(relationship.Morale  - UnityEngine.Random.Range(0, 5), 0, 100);

                // TODO: if is CEO in own project, morale loss is way lower or zero
                bool isOwner = human.hasCEO;
                if (isOwner)
                    newLoyalty = 100;


                human.ReplaceHumanCompanyRelationship(newAdaptation, newLoyalty);

                if (c.hasProduct)
                {
                    var niche = c.product.Niche;
                    // increase expertise too

                    var newExpertise = 1;
                    if (human.humanSkills.Expertise.ContainsKey(niche))
                        newExpertise = Mathf.Clamp(human.humanSkills.Expertise[niche] + 1, 0, 100);

                    human.humanSkills.Expertise[niche] = newExpertise;
                }
            }
        }

        // crunching
        var products = contexts.game.GetEntities(GameMatcher.Product);

        for (var i = 0; i < products.Length; i++)
        {
            var change = products[i].isCrunching ? -4 : 2;

            var workers = Teams.GetAmountOfWorkers(products[i], gameContext) + 1;
            var required = Products.GetNecessaryAmountOfWorkers(products[i], gameContext);

            if (required > workers)
                change -= required / workers;


            products[i].team.Morale = Mathf.Clamp(products[i].team.Morale + change, 0, 100);
        }
    }
}

using System.Collections.Generic;
using Assets.Classes;
using Assets.Utils;
using Entitas;
using UnityEngine;

class ProductResourceSystems : ReactiveSystem<GameEntity>
{
    private readonly Contexts contexts;

    public ProductResourceSystems(Contexts contexts) : base(contexts.game)
    {
        // TODO: Add proper IGroups!
        this.contexts = contexts;
    }

    bool IsMonthEnd(DateComponent dateComponent)
    {
        return dateComponent.Date % 30 == 0 && dateComponent.Date > 0;
    }

    float GetConversion(GameEntity e)
    {
        int productLoyalty = e.product.ProductLevel * 5;
        int bugsPenalty = 0;
        int pricingPenalty = 0;

        int loyalty = productLoyalty - bugsPenalty - pricingPenalty;

        return Mathf.Pow(loyalty, 0.5f);
    }

    void AddResources(GameEntity[] Products)
    {
        foreach (var e in Products)
        {
            var team = e.team;

            var ideas = 100;

            long money = ProductEconomicsUtils.GetIncome(e);

            var resources = new TeamResource(
                team.Programmers * Constants.DEVELOPMENT_PRODUCTION_PROGRAMMER,
                team.Managers * Constants.DEVELOPMENT_PRODUCTION_MANAGER,
                team.Marketers * Constants.DEVELOPMENT_PRODUCTION_MARKETER,
                ideas,
                money
                );

            e.product.Resources.Add(resources);
        }
    }

    protected override void Execute(List<GameEntity> entities)
    {
        if (!IsMonthEnd(entities[0].date))
            return;

        Debug.Log("Execute resource system");

        GameEntity[] Products = contexts.game
            .GetEntities(GameMatcher.AllOf(GameMatcher.Product, GameMatcher.Marketing));

        AddResources(Products);
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
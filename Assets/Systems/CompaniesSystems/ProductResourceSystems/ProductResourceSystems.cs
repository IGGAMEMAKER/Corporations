using System.Collections.Generic;
using Assets.Classes;
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

    long GetIncome(GameEntity e)
    {
        int basePayments = 15;
        int pricing = 1;
        int price = basePayments * pricing;
        long payments = price;

        return e.marketing.Clients * payments;
    }

    void AddResources(GameEntity[] Products)
    {
        foreach (var e in Products)
        {
            var team = e.team;

            var baseProduction = 15;
            var ideas = 100;

            long money = GetIncome(e);

            var resources = new TeamResource(
                team.Programmers * baseProduction,
                team.Managers * baseProduction,
                team.Marketers * baseProduction,
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
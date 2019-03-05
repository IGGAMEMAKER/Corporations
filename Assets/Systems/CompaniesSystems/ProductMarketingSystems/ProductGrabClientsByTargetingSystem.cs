using System.Collections.Generic;
using Entitas;
using UnityEngine;

class ProductGrabClientsByTargetingSystem : ReactiveSystem<GameEntity>
{
    private readonly Contexts contexts;

    public ProductGrabClientsByTargetingSystem(Contexts contexts) : base(contexts.game)
    {
        // TODO: Add proper IGroups!
        this.contexts = contexts;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        GameEntity[] Products = contexts.game
            .GetEntities(GameMatcher
                .AllOf(GameMatcher.Marketing, GameMatcher.Targeting));

        uint baseForNiche = 100;
        // TODO Calculate proper base value!

        foreach (var e in Products)
        {
            uint brandModifier = (uint) e.marketing.BrandPower * 20;

            Debug.Log("Grabbing more clients! brand modifier: " + brandModifier + "%");

            e.marketing.Clients += baseForNiche * (100 + brandModifier) / 100;
        }
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

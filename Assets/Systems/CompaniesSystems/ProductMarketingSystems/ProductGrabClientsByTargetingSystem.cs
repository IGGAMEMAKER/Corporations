using System.Collections.Generic;
using Assets.Classes;
using Entitas;

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
            .GetEntities(GameMatcher.AllOf(GameMatcher.Marketing, GameMatcher.Targeting));

        uint baseForNiche = 100;
        long adCost = 10000;
        // TODO Calculate proper base value!

        foreach (var e in Products)
        {
            uint brandModifier = (uint) e.marketing.BrandPower * 20;
            
            e.marketing.Clients += baseForNiche * (100 + brandModifier) / 100;
            e.product.Resources.Spend(new TeamResource(0, 0, 0, 0, adCost));
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
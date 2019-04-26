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

        long baseForNiche = 100;
        long adCost = 700;
        // TODO Calculate proper base value!

        TeamResource need = new TeamResource(0, 0, 0, 0, adCost);

        foreach (var e in Products)
        {
            if (e.companyResource.Resources.IsEnoughResources(need))
            {
                long brandModifier = e.marketing.BrandPower * 20;

                e.marketing.Segments[UserType.Newbie] += baseForNiche * (100 + brandModifier) / 100;
                e.companyResource.Resources.Spend(need);
            }
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
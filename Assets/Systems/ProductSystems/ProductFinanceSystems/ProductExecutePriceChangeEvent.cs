using System.Collections.Generic;
using Assets.Utils;
using Entitas;

public class ProductExecutePriceChangeEvent : ReactiveSystem<GameEntity>
{
    readonly GameContext _context;

    public ProductExecutePriceChangeEvent(Contexts contexts) : base(contexts.game)
    {
        _context = contexts.game;
    }

    void ChangePrice(GameEntity e)
    {
        var newPrice = e.eventFinancePricingChange.level;

        ProductUtils.SetPrice(e, newPrice);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.Finance, GameMatcher.EventFinancePricingChange));
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasEventFinancePricingChange && entity.hasFinance;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            ChangePrice(e);

            e.RemoveEventFinancePricingChange();
        }
    }
}

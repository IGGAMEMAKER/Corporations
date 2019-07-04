using System.Collections.Generic;
using Entitas;

public class ProductToggleTargetingHandlerSystem : ReactiveSystem<GameEntity>
{
    private readonly Contexts contexts;

    public ProductToggleTargetingHandlerSystem(Contexts contexts) : base(contexts.game)
    {
        this.contexts = contexts;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            e.isTargeting = !e.isTargeting;
            e.RemoveEventMarketingEnableTargeting();
        }
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasEventMarketingEnableTargeting;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.EventMarketingEnableTargeting);
    }
}

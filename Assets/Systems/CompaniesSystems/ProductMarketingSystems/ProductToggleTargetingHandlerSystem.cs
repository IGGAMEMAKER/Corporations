using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class ProductToggleTargetingHandlerSystem : ReactiveSystem<GameEntity>
{
    private readonly Contexts contexts;

    public ProductToggleTargetingHandlerSystem(Contexts contexts) : base(contexts.game)
    {
        this.contexts = contexts;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        Debug.Log("ProductToggleTargetingHandlerSystem ReactiveSystem!");

        foreach (var e in entities)
        {
            Debug.Log("Toggle Targeting!");

            e.isTargeting = true;
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

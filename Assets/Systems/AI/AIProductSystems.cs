using Entitas;
using System.Collections.Generic;


public abstract class OnDateChange : ReactiveSystem<GameEntity>
{
    public readonly Contexts contexts;
    public readonly GameContext gameContext;

    protected OnDateChange(Contexts contexts) : base (contexts.game)
    {
        this.contexts = contexts;
        gameContext = contexts.game;
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasDate;
    }

    //bool IsMonthEnd(DateComponent dateComponent)
    //{
    //    return dateComponent.Date % 30 == 0 && dateComponent.Date > 0;
    //}

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Date);
    }
}

public class AIProductSystems : OnDateChange
{
    public AIProductSystems(Contexts contexts) : base(contexts)
    {

    }

    protected override void Execute(List<GameEntity> entities)
    {
        var AIProducts = gameContext.GetEntities(GameMatcher
            .AllOf(GameMatcher.Product)
            .NoneOf(GameMatcher.ControlledByPlayer)
            );

        foreach (var e in AIProducts)
        {
            
        }
    }
}

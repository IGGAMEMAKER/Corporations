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

    //public abstract bool InternalFilter(GameEntity gameEntity);
    //public abstract bool InternalTrigger(IContext<GameEntity> context);

    //protected override void Execute(List<GameEntity> entities)
    //{

    //}

    //bool IsMonthEnd(DateComponent dateComponent)
    //{
    //    return dateComponent.Date % 30 == 0 && dateComponent.Date > 0;
    //}

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasDate;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Date);
    }
}

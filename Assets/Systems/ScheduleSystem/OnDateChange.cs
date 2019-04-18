using Assets.Utils;
using Entitas;

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

public abstract class OnMonthChange : ReactiveSystem<GameEntity>
{
    public readonly Contexts contexts;
    public readonly GameContext gameContext;

    protected OnMonthChange(Contexts contexts) : base (contexts.game)
    {
        this.contexts = contexts;
        gameContext = contexts.game;
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasDate && entity.date.Date % 30 == 0 && entity.date.Date > 0;
    }

    //public abstract ICollector<GameEntity> GetCollector(IContext<GameEntity> context);

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        //return GetCollector(context);
        return context.CreateCollector(GameMatcher.Date);
    }
}

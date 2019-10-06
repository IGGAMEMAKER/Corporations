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

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasDate;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Date);
    }
}


public abstract class OnRandomDateChange : ReactiveSystem<GameEntity>
{
    public readonly Contexts contexts;
    public readonly GameContext gameContext;

    protected OnRandomDateChange(Contexts contexts) : base(contexts.game)
    {
        this.contexts = contexts;
        gameContext = contexts.game;
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasDate && entity.date.Date % AmountOfDays== 0 && entity.date.Date > 0;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Date);
    }

    public abstract int AmountOfDays { get; }
}

public abstract class OnHalfYear : OnRandomDateChange
{
    protected OnHalfYear(Contexts contexts) : base(contexts)
    {
    }

    public override int AmountOfDays => 180;
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

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Date);
    }
}

public abstract class OnQuarterChange : ReactiveSystem<GameEntity>
{
    public readonly Contexts contexts;
    public readonly GameContext gameContext;

    protected OnQuarterChange(Contexts contexts) : base (contexts.game)
    {
        this.contexts = contexts;
        gameContext = contexts.game;
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasDate && entity.date.Date % 90 == 0 && entity.date.Date > 0;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Date);
    }
}

public abstract class OnYearChange : ReactiveSystem<GameEntity>
{
    public readonly Contexts contexts;
    public readonly GameContext gameContext;

    protected OnYearChange(Contexts contexts) : base (contexts.game)
    {
        this.contexts = contexts;
        gameContext = contexts.game;
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasDate && entity.date.Date % 360 == 0 && entity.date.Date > 0;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Date);
    }
}

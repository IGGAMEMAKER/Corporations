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

public abstract class OnPeriodChange : OnWeekChange
{
    public OnPeriodChange(Contexts contexts) : base(contexts)
    {
    }
}

public abstract class OnHalfYear : OnRandomDateChange
{
    public OnHalfYear(Contexts contexts) : base(contexts)
    {
    }

    public override int AmountOfDays => 180;
}

public abstract class OnMonthChange : OnRandomDateChange
{
    public OnMonthChange(Contexts contexts) : base (contexts)
    {
    }

    public override int AmountOfDays => 30;
}

public abstract class OnWeekChange : OnRandomDateChange
{
    public OnWeekChange(Contexts contexts) : base (contexts)
    {
    }

    public override int AmountOfDays => 7;
}

public abstract class OnQuarterChange : OnRandomDateChange
{
    public OnQuarterChange(Contexts contexts) : base(contexts)
    {
    }

    public override int AmountOfDays => 90;
}

public abstract class OnYearChange : OnRandomDateChange
{
    public OnYearChange(Contexts contexts) : base(contexts)
    {
    }

    public override int AmountOfDays => 360;
}
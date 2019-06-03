using System.Collections.Generic;
using Assets.Utils;
using Entitas;

// TODO replace with OnMonthChange!
class ProductResourceSystems : OnDateChange
{
    public ProductResourceSystems(Contexts contexts) : base(contexts)
    {
    }

    bool IsPeriodEnd(DateComponent dateComponent)
    {
        int period = CompanyEconomyUtils.GetPeriodDuration();

        if (period == 1)
            return true;

        return dateComponent.Date % period == 0 && dateComponent.Date > 0;
    }

    bool IsMonthEnd(DateComponent dateComponent)
    {
        return dateComponent.Date % 30 == 0 && dateComponent.Date > 0;
    }

    bool IsWeekEnd(DateComponent dateComponent)
    {
        return dateComponent.Date % 7 == 0 && dateComponent.Date > 0;
    }



    void AddResources(GameEntity[] Products)
    {
        foreach (var e in Products)
        {
            var resources = CompanyEconomyUtils.GetResourceChange(e, contexts.game);

            CompanyUtils.AddResources(e, resources);
        }
    }

    protected override void Execute(List<GameEntity> entities)
    {
        if (IsPeriodEnd(entities[0].date))
            AddResources(contexts.game.GetEntities(GameMatcher.Product));
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
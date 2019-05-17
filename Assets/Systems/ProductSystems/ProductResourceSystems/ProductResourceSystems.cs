using System.Collections.Generic;
using Assets.Classes;
using Assets.Utils;
using Entitas;

// TODO replace with OnMonthChange!
class ProductResourceSystems : OnDateChange
{
    int period = 1;

    public ProductResourceSystems(Contexts contexts) : base(contexts)
    {
    }

    bool IsPeriodEnd(DateComponent dateComponent)
    {
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
            var team = e.team;

            var ideas = 3 * period;

            long money = CompanyEconomyUtils.GetCompanyIncome(e, contexts.game) * period / 30;

            var resources = new TeamResource(
                team.Programmers * Constants.DEVELOPMENT_PRODUCTION_PROGRAMMER * period,
                team.Managers * Constants.DEVELOPMENT_PRODUCTION_MANAGER * period,
                team.Marketers * Constants.DEVELOPMENT_PRODUCTION_MARKETER * period,
                ideas,
                money
                );

            e.companyResource.Resources.Add(resources);
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
using System.Collections.Generic;
using Assets.Utils;

class ProductResourceSystems : OnPeriodChange
{
    public ProductResourceSystems(Contexts contexts) : base(contexts)
    {
    }

    protected override void Execute(List<GameEntity> entities)
    {
        var products = CompanyUtils.GetProductCompanies(gameContext);

        foreach (var e in products)
        {
            var resources = EconomyUtils.GetProductCompanyResourceChange(e, contexts.game);

            CompanyUtils.AddResources(e, resources);
        }
    }
}
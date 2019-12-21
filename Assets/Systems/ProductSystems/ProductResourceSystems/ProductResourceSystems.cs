using System.Collections.Generic;
using Assets.Utils;

class ProductResourceSystem : OnPeriodChange
{
    public ProductResourceSystem(Contexts contexts) : base(contexts)
    {
    }

    protected override void Execute(List<GameEntity> entities)
    {
        var products = Companies.GetProductCompanies(gameContext);

        foreach (var e in products)
        {
            var resources = Economy.GetProductCompanyResourceChange(e, contexts.game);

            Companies.AddResources(e, resources);
        }
    }
}
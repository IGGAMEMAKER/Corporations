using Assets.Core;
using System.Collections.Generic;
using System.Linq;

public partial class AutoUpgradeProductsSystem : OnDateChange
{
    public AutoUpgradeProductsSystem(Contexts contexts) : base(contexts) {}

    protected override void Execute(List<GameEntity> entities)
    {
        var products = Companies.GetProductCompanies(gameContext);

        foreach (var product in products)
            Products.UpdgradeProduct(product, gameContext);


        var releasableAIApps = products
            // increase performance
            .Where(p => !p.isRelease)
            .Where(p => Companies.IsReleaseableApp(p, gameContext))
            
            .Where(p => !Companies.IsRelatedToPlayer(gameContext, p))
            ;

        foreach (var concept in releasableAIApps)
            MarketingUtils.ReleaseApp(concept, gameContext);
    }
}
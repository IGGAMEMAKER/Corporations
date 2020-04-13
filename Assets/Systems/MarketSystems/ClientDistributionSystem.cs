using System.Collections.Generic;
using Assets.Core;

public partial class ClientDistributionSystem : OnMonthChange
{
    public ClientDistributionSystem(Contexts contexts) : base(contexts) {}

    protected override void Execute(List<GameEntity> entities)
    {
        var niches = Markets.GetNiches(gameContext);

        foreach (var n in niches)
            CheckMarket(n);
    }

    void CheckMarket(GameEntity niche)
    {
        var nicheType = niche.niche.NicheType;

        var products = Markets.GetProductsOnMarket(gameContext, nicheType, false);

        foreach (var p in products)
        {
            var powerChange = Marketing.GetBrandChange(p, gameContext).Sum();

            Marketing.AddBrandPower(p, powerChange);
            Marketing.AddClients(p, Marketing.GetAudienceGrowth(p, gameContext));
        }
    }
}


/*
 ICrunchingListener
 IMarketingListener
 IProductListener
 ICompanyResourceListener ??
 ITargetingListener
 IReleaseListener
 ICompanyGoalListener
*/

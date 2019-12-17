using System.Collections.Generic;
using System.Linq;
using Assets.Utils;

public partial class ClientDistributionSystem : OnPeriodChange
{
    public ClientDistributionSystem(Contexts contexts) : base(contexts)
    {
    }

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

        ChurnUsers(products, niche);
        ChangeBrandPowers(products);

        DistributeClients(products, niche);
    }

    void ChurnUsers(GameEntity[] products, GameEntity niche)
    {
        return;
        //var clientContainers = niche.nicheClientsContainer.Clients;

        var dumpingCompanies = products.Where(p => p.isDumping);

        var totalBrands = dumpingCompanies.Sum(p => p.branding.BrandPower);

        for (var i = 0; i < products.Length; i++)
        {
            var p = products[i];

            var churnClients = MarketingUtils.GetChurnClients(contexts.game, p.company.Id);
            MarketingUtils.AddClients(p, -churnClients);

            // send churn users to dumping companies
            foreach (var d in dumpingCompanies)
            {
                float clients = churnClients;
                if (totalBrands == 0)
                    clients /= dumpingCompanies.Count();
                else
                    clients *= p.branding.BrandPower / totalBrands;

                MarketingUtils.AddClients(d, (long)(clients));
            }
        }
    }

    void ChangeBrandPowers(GameEntity[] products)
    {
        for (var i = 0; i < products.Length; i++)
        {
            var powerChange = MarketingUtils.GetBrandChange(products[i], gameContext).Sum();

            MarketingUtils.AddBrandPower(products[i], (float)powerChange);
        }
    }

    void DistributeClients(GameEntity[] products, GameEntity niche)
    {
        var clientContainers = niche.nicheClientsContainer.Clients;

        foreach (var p in products)
        {
            var clients = MarketingUtils.GetAudienceGrowth(p, gameContext);

            MarketingUtils.AddClients(p, clients);

            var segId = p.productPositioning.Positioning;
            clientContainers[segId] -= clients;
        }

        niche.ReplaceNicheClientsContainer(clientContainers);
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

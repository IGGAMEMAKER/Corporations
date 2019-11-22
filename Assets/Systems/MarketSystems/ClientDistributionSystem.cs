using System.Collections.Generic;
using System.Linq;
using Assets.Utils;
using UnityEngine;

public partial class ClientDistributionSystem : OnPeriodChange
{
    public ClientDistributionSystem(Contexts contexts) : base(contexts)
    {
    }

    protected override void Execute(List<GameEntity> entities)
    {
        var niches = NicheUtils.GetNiches(gameContext);

        foreach (var n in niches)
            CheckMarket(n);
    }

    void CheckMarket(GameEntity niche)
    {
        var nicheType = niche.niche.NicheType;

        var products = NicheUtils.GetProductsOnMarket(gameContext, nicheType, false);

        ChurnUsers(products, niche);
        ChangeBrandPowers(products);

        DistributeClients(products, niche);
    }

    void ChurnUsers(GameEntity[] products, GameEntity niche)
    {
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
            var powerChange = MarketingUtils.GetMonthlyBrandPowerChange(products[i], gameContext).Sum();

            MarketingUtils.AddBrandPower(products[i], powerChange);
        }
    }

    void DistributeClients(GameEntity[] products, GameEntity niche)
    {
        var clientContainers = niche.nicheClientsContainer.Clients;

        for (var i = 0; i < products.Length; i++)
        {
            var p = products[i];

            var clients = GetCompanyAudienceReach(p);

            MarketingUtils.AddClients(p, clients);

            var segId = p.productPositioning.Positioning;
            clientContainers[segId] -= clients;
        }

        niche.ReplaceNicheClientsContainer(clientContainers);
    }

    long GetCompanyAudienceReach(GameEntity product)
    {
        var rand = Random.Range(Constants.CLIENT_GAIN_MODIFIER_MIN, Constants.CLIENT_GAIN_MODIFIER_MAX);

        var growth = MarketingUtils.GetAudienceGrowth(product, gameContext);

        return (long)(growth * rand);
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

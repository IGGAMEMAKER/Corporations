using System.Collections.Generic;
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

        ChurnUsers(products);
        ChangeBrandPowers(products);

        long flow = MarketingUtils.GetCurrentClientFlow(gameContext, nicheType);

        var clientContainers = niche.nicheClientsContainer.Clients;

        for (var i = 0; i < products.Length; i++)
        {
            var p = products[i];
            var segId = p.productPositioning.Positioning;

            var clients = GetCompanyAudienceReach(p, flow);

            var clientCap = flow * 10;
            if (clients > clientCap)
                clients = clientCap;

            MarketingUtils.AddClients(p, clients);

            clientContainers[segId] -= clients;
        }

        niche.ReplaceNicheClientsContainer(clientContainers);
    }

    void ChurnUsers(GameEntity[] products)
    {
        for (var i = 0; i < products.Length; i++)
        {
            var p = products[i];

            var churnClients = MarketingUtils.GetChurnClients(contexts.game, p.company.Id);

            MarketingUtils.AddClients(p, -churnClients);
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



    long GetCompanyAudienceReach(GameEntity product, long flow)
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

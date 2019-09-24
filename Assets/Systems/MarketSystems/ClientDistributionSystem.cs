using System.Collections.Generic;
using Assets.Utils;
using Entitas;
using UnityEngine;

public partial class ClientDistributionSystem : OnMonthChange
{
    public ClientDistributionSystem(Contexts contexts) : base(contexts)
    {
    }

    protected override void Execute(List<GameEntity> entities)
    {
        //var niches = NicheUtils.GetNiches(gameContext);
        var niches = NicheUtils.GetPlayableNiches(gameContext);

        foreach (var n in niches)
            CheckMarket(n);
    }

    void CheckMarket(GameEntity niche)
    {
        var nicheType = niche.niche.NicheType;

        long flow = MarketingUtils.GetCurrentClientFlow(gameContext, nicheType);

        //// calculate churn rates here?

        // we have added all users at once
        //NicheUtils.AddNewUsersToMarket(niche, gameContext, flow);

        var clientContainers = niche.nicheClientsContainer.Clients;

        var products = NicheUtils.GetProductsOnMarket(gameContext, nicheType, false);

        var segments = NicheUtils.GetNichePositionings(nicheType, gameContext);

        for (var i = 0; i < products.Length; i++)
        {
            var p = products[i];
            var segId = p.productPositioning.Positioning;

            var clients = GetCompanyAudienceReach(p, flow);

            var clientCap = flow * 10;
            if (clients > clientCap)
                clients = clientCap;

            MarketingUtils.AddClients(p, clients);

            //
            clientContainers[segId] -= clients;
        }

        for (var i = 0; i < products.Length; i++)
        {
            var powerChange = MarketingUtils.GetMonthlyBrandPowerChange(products[i], gameContext).Sum();

            MarketingUtils.AddBrandPower(products[i], powerChange);
        }

        niche.ReplaceNicheClientsContainer(clientContainers);
    }

    long GetCompanyAudienceReach(GameEntity product, long flow)
    {
        //var rand = Random.Range(0.15f, 1.4f);
        var rand = Random.Range(0.25f, 1.2f);

        var SEO = (product.branding.BrandPower + 100) / 100;

        var marketing = 0;
        if (TeamUtils.IsUpgradePicked(product, TeamUpgrade.BaseMarketing))
            marketing = 1;
        if (TeamUtils.IsUpgradePicked(product, TeamUpgrade.AllPlatformMarketing))
            marketing = 3;

        if (TeamUtils.IsUpgradePicked(product, TeamUpgrade.AggressiveMarketing))
            marketing *= 3;

        //     0...100 + 12...60
        var multiplier = SEO + marketing * 3; // + rand * 50;

        return (long)(multiplier * flow);
    }
}

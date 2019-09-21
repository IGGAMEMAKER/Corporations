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

        // calculate churn rates here?

        NicheUtils.AddNewUsersToMarket(niche, gameContext, flow);

        var clientContainers = niche.nicheClientsContainer.Clients;

        var products = NicheUtils.GetProductsOnMarket(gameContext, nicheType, false);

        var segments = NicheUtils.GetNichePositionings(nicheType, gameContext);

        var strengths = new float[segments.Count];
        var strengthsProducts = new Dictionary<int, float>();

        // calculate relative strengths
        for (var i = 0; i < products.Length; i++)
        {
            var p = products[i];
            var segId = p.productPositioning.Positioning;

            var reach = GetCompanyAudienceReach(p);

            strengths[segId] += reach;

            strengthsProducts[p.company.Id] = reach;
        }

        for (var i = 0; i < products.Length; i++)
        {
            var p = products[i];
            var segId = p.productPositioning.Positioning;

            var segmentClients = clientContainers[segId];

            var productStrength = strengthsProducts[p.company.Id];
            var totalStrengths = strengths[segId];

            var relativeCompanyStrength = productStrength / totalStrengths;


            var clients = (long)(segmentClients * relativeCompanyStrength);

            var clientCap = flow * 4;
            if (clients > clientCap)
                clients = clientCap;

            MarketingUtils.AddClients(p, clients);

            //
            strengths[segId] -= productStrength;

            clientContainers[segId] -= clients;
        }

        for (var i = 0; i < products.Length; i++)
        {
            var powerChange = RecalculateBrandPowers(products[i], niche);
            MarketingUtils.AddBrandPower(products[i], powerChange);

            PayForMarketing(products[i], niche);
        }

        niche.ReplaceNicheClientsContainer(clientContainers);
    }

    float GetMarketShareBasedBrandDecay(GameEntity product)
    {
        var marketShare = (float)CompanyUtils.GetMarketShareOfCompanyMultipliedByHundred(product, gameContext);
        var brand = product.branding.BrandPower;

        var change = (marketShare - brand) / 10;

        return change;

        return Mathf.Min(change, 0); // -10....0
    }

    float RecalculateBrandPowers(GameEntity product, GameEntity niche)
    {
        bool isPayingForMarketing = CompanyEconomyUtils.IsCanAffordMarketing(product, gameContext);

        Debug.Log("RecalculateBrandPowers: " + product.company.Name + " isPayingForMarketing=" + isPayingForMarketing);

        float isOutOfMarket = ProductUtils.IsOutOfMarket(product, gameContext) ? -1f : 0;
        float innovationBonus = product.isTechnologyLeader ? 2f : 0;
        if (isPayingForMarketing)
            innovationBonus *= 4;

        var appQualityModifier = isOutOfMarket + innovationBonus;

        var decay = GetMarketShareBasedBrandDecay(product);
        var paymentModifier = isPayingForMarketing ? 1f : 0;

        var power = -1 + decay + appQualityModifier + paymentModifier;

        Debug.Log("Power change for " + product.company.Name + " is " + power);

        return power;
    }

    void PayForMarketing(GameEntity product, GameEntity niche)
    {
        if (CompanyEconomyUtils.IsCanAffordMarketing(product, gameContext))
        {
            var maintenance = NicheUtils.GetBaseMarketingMaintenance(niche);
            CompanyUtils.SpendResources(product, maintenance);
        }

        var aggressiveMaintenance = NicheUtils.GetAggressiveMarketingMaintenance(niche);
        if (CompanyUtils.IsEnoughResources(product, aggressiveMaintenance))
        {
            CompanyUtils.SpendResources(product, aggressiveMaintenance);
            MarketingUtils.AddBrandPower(product, 10);
        }
    }

    float GetCompanyAudienceReach(GameEntity product)
    {
        //var rand = Random.Range(0.15f, 1.4f);
        var rand = Random.Range(0.25f, 1.2f);

        //     0...100 + 12...60
        return product.branding.BrandPower + rand * 50;
    }
}

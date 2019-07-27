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
        var niches = NicheUtils.GetNiches(gameContext);

        foreach (var n in niches)
            CheckMarket(n);
    }

    void CheckMarket(GameEntity niche)
    {
        var nicheType = niche.niche.NicheType;

        long flow = MarketingUtils.GetCurrentClientFlow(gameContext, nicheType);

        var segments = NicheUtils.GetNichePositionings(nicheType, gameContext);

        var clientContainers = niche.nicheClientsContainer.Clients;
        foreach (var s in segments)
        {
            // clients to clientContainer

            var segId = s.Key;

            var share = s.Value.marketShare;

            clientContainers[segId] += flow * share / 100;
        }

        var products = NicheUtils.GetPlayersOnMarket(gameContext, nicheType, false);

        var strengths = new float[segments.Count];
        var strengthsProducts = new Dictionary<int, float>();

        // calculate relative strengths
        for (var i = 0; i < products.Length; i++)
        {
            var p = products[i];
            var segId = p.productPositioning.Positioning;

            var reach = GetCompanyAudienceReach(p);

            strengths[segId] += reach;

            var totalStrengths = strengths[segId];

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

            MarketingUtils.AddClients(p, UserType.Core, clients);

            //
            strengths[segId] -= productStrength;

            clientContainers[segId] -= clients;
        }

        niche.ReplaceNicheClientsContainer(clientContainers);
    }

    void PayForMarketing(GameEntity product)
    {

    }

    float GetCompanyAudienceReach(GameEntity product)
    {
        var rand = Random.Range(0.15f, 1.4f);

        var targetingMultiplier = 1;
        var brandingMultiplier = 10;

        bool isBrandingPossible = !MarketingUtils.HasBrandingCooldown(product);

        var totalMultplier = targetingMultiplier + (isBrandingPossible ? brandingMultiplier : 0);

        var companyMultiplier = MarketingUtils.GetCompanyReachModifierMultipliedByHundred(product);

        var reach = rand * totalMultplier * companyMultiplier;

        return reach;
    }
}

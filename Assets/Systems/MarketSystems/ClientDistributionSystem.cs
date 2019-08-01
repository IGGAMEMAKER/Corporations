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

        NicheUtils.AddNewUsersToMarket(niche, gameContext, flow);

        var clientContainers = niche.nicheClientsContainer.Clients;

        var products = NicheUtils.GetPlayersOnMarket(gameContext, nicheType, false);

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

            MarketingUtils.AddClients(p, UserType.Core, clients);

            //
            strengths[segId] -= productStrength;

            clientContainers[segId] -= clients;

            PayForMarketing(p);
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

        bool wantsToMakeBrandingCampaign = Random.Range(0, 7) < 2;
        bool isBrandingPossible = !MarketingUtils.HasBrandingCooldown(product) && wantsToMakeBrandingCampaign;

        var totalMultplier = targetingMultiplier + (isBrandingPossible ? brandingMultiplier : 0);

        var companyMultiplier = MarketingUtils.GetCompanyReachModifierMultipliedByHundred(product);

        var reach = 1 + rand * totalMultplier * companyMultiplier;

        return reach;
    }
}

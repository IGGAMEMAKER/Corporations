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

            MarketingUtils.AddClients(p, clients);

            //
            strengths[segId] -= productStrength;

            clientContainers[segId] -= clients;
        }

        for (var i = 0; i < products.Length; i++)
            PayForMarketing(products[i], niche);

        niche.ReplaceNicheClientsContainer(clientContainers);
    }

    void PayForMarketing(GameEntity product, GameEntity niche)
    {
        var maintenance = NicheUtils.GetBaseMarketingMaintenance(niche);

        //Debug.Log("Pay for marketing : " + product.company.Name);
        //maintenance.Print();

        bool isPayingForMarketing = CompanyEconomyUtils.IsCanAffordMarketing(product, gameContext);

        var power = -1;

        if (isPayingForMarketing)
            power += 4;

        float isOutOfMarket = ProductUtils.IsOutOfMarket(product, gameContext) ? -5f : 0;
        float innovationBonus = product.isTechnologyLeader ? 3f : 0;



        MarketingUtils.AddBrandPower(product, power);

        if (isPayingForMarketing)
            CompanyUtils.SpendResources(product, maintenance);
    }

    float GetCompanyAudienceReach(GameEntity product)
    {
        var rand = Random.Range(0.15f, 1.4f);

        return 1 + rand * product.branding.BrandPower;
    }
}

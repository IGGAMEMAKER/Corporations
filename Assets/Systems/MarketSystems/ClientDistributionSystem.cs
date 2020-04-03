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

        ChangeBrandPowers(products);

        //DistributeClients(products, niche);
    }

    void ChangeBrandPowers(GameEntity[] products)
    {
        for (var i = 0; i < products.Length; i++)
        {
            var powerChange = Marketing.GetBrandChange(products[i], gameContext).Sum();

            Marketing.AddBrandPower(products[i], (float)powerChange);
        }
    }

    void DistributeClients(GameEntity[] products, GameEntity niche)
    {
        var clientContainers = niche.nicheClientsContainer.Clients;

        foreach (var p in products)
        {
            var clients = Marketing.GetAudienceGrowth(p, gameContext);

            Marketing.AddClients(p, clients);

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

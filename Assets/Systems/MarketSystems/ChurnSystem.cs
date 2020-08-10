using System.Collections.Generic;
using System.Linq;
using Assets.Core;

public partial class ChurnSystem : OnPeriodChange
{
    public ChurnSystem(Contexts contexts) : base(contexts) {}

    protected override void Execute(List<GameEntity> entities)
    {
        var niches = Markets.GetNiches(gameContext);

        foreach (var n in niches)
        {
            var products = Markets.GetProductsOnMarket(gameContext, n.niche.NicheType, false);

            //ChurnUsers(products, n);
        }
    }

    void ChurnUsers(GameEntity[] products, GameEntity niche)
    {
        var clientContainers = niche.nicheClientsContainer.Clients;

        var dumpingCompanies = products.Where(p => p.isDumping);

        var totalBrands = dumpingCompanies.Sum(p => p.branding.BrandPower);

        for (var i = 0; i < products.Length; i++)
        {
            var p = products[i];

            var audiences = Marketing.GetAudienceInfos();

            foreach (var a in audiences)
            {
                var churnClients = Marketing.GetChurnClients(contexts.game, p.company.Id, a.ID);
                Marketing.AddClients(p, -churnClients, a.ID);
            }

            //clientContainers[0] += churnClients;

            continue;
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

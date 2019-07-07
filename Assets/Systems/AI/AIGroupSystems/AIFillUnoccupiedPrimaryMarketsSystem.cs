using System.Collections.Generic;
using System.Linq;
using Assets.Utils;
using UnityEngine;

public class AIFillUnoccupiedPrimaryMarketsSystem : OnQuarterChange
{
    public AIFillUnoccupiedPrimaryMarketsSystem(Contexts contexts) : base(contexts) { }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var c in CompanyUtils.GetAIManagingCompanies(gameContext))
            CheckMarkets(c);
    }

    void CheckMarkets(GameEntity managingCompany)
    {
        foreach (var n in managingCompany.companyFocus.Niches)
        {
            if (HasCompanyOnMarket(managingCompany, n))
                continue;

            BuyOrCreate(managingCompany, n);
        }
    }

    private void BuyOrCreate(GameEntity managingCompany, NicheType n)
    {
        var p = CompanyUtils.AutoGenerateProductCompany(n, gameContext);

        CompanyUtils.AttachToGroup(gameContext, managingCompany.company.Id, p.company.Id);
        Debug.Log("BuyOrCreateCompany on market");
    }

    bool HasCompanyOnMarket(GameEntity group, NicheType nicheType)
    {
        return CompanyUtils.GetDaughterCompanies(gameContext, group.company.Id).Count(c => c.hasProduct && c.product.Niche == nicheType) > 0;
    }
}

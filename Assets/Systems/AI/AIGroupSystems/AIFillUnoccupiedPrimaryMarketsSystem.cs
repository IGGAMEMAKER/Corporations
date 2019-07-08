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
        var niches = managingCompany.companyFocus.Niches;

        niches
            .Select(n => !HasCompanyOnMarket(managingCompany, n));
            //BuyOrCreate(managingCompany, n);
    }

    private void BuyOrCreate(GameEntity managingCompany, NicheType n)
    {
        var p = CompanyUtils.AutoGenerateProductCompany(n, gameContext);

        CompanyUtils.AttachToGroup(gameContext, managingCompany.company.Id, p.company.Id);
        Debug.Log("BuyOrCreateCompany on market");
    }

    bool HasCompanyOnMarket(GameEntity group, NicheType nicheType)
    {
        return CompanyUtils.HasCompanyOnMarket(group, nicheType, gameContext);
    }
}

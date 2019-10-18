using System.Collections.Generic;
using System.Linq;
using Assets.Utils;
using UnityEngine;

public partial class AIManageGroupSystems : OnQuarterChange
{
    void FillUnoccupiedMarkets(GameEntity managingCompany)
    {
        foreach (var n in GetUnoccupiedNiches(managingCompany))
            OccupyNiche(n, managingCompany);
    }

    void OccupyNiche(NicheType n, GameEntity managingCompany)
    {
        var products = NicheUtils.GetProductsAvailableForSaleOnMarket(n, gameContext);

        var candidates = GetAcquisitionCandidates(products, managingCompany);

        Debug.Log("Check unoccupied niche " + n + ". Candidates: " + candidates.Count());

        if (candidates.Count() > 0)
        {
            foreach (var c in candidates)
                SendAcquisitionOffer(managingCompany, c, gameContext);
        }
        else
            CreateCompanyOnMarket(n, managingCompany);
    }

    void CreateCompanyOnMarket(NicheType n, GameEntity managingCompany)
    {
        CompanyUtils.CreateProductAndAttachItToGroup(gameContext, n, managingCompany);
    }

    IEnumerable<NicheType> GetUnoccupiedNiches(GameEntity managingCompany)
    {
        var niches = managingCompany.companyFocus.Niches;

        return niches.Where(n => !HasCompanyOnMarket(managingCompany, n));
    }

    IEnumerable<GameEntity> GetAcquisitionCandidates(GameEntity[] products, GameEntity managingCompany)
    {
        return products
            .Where(p => p.isIndependentCompany)
            .Where(p => CompanyUtils.GetFounderAmbition(p, gameContext) == Ambition.EarnMoney);
                //.Where(p => CompanyUtils.IsWillBuyCompany(managingCompany, p, gameContext))
                //.OrderByDescending(p => GetCompanyAcquisitionPriority(managingCompany, p, gameContext));
    }

    long GetCompanyAcquisitionPriority(GameEntity buyer, GameEntity target, GameContext gameContext)
    {
        var price = EconomyUtils.GetCompanySellingPrice(gameContext, target.company.Id);
        var desireToBuy = CompanyUtils.GetDesireToBuy(buyer, target, gameContext);

        var modifiers = Random.Range(10, 14);

        var priority = modifiers * desireToBuy / (10 * (price + 1));

        //Debug.Log($"Priority of {target.company.Name} in {buyer.company.Name}'s eyes: {priority}");

        return priority;
    }

    void SendAcquisitionOffer(GameEntity buyer, GameEntity target, GameContext gameContext)
    {
        var cost = EconomyUtils.GetCompanyCost(gameContext, target.company.Id) * Random.Range(1, 10) / 2;

        if (!CompanyUtils.IsEnoughResources(buyer, new Assets.Classes.TeamResource(cost)))
            return;

        Debug.Log("AI.SendAcquisitionOffer: " + buyer.company.Name + " wants " + target.company.Name);

        CompanyUtils.SendAcquisitionOffer(gameContext, target.company.Id, buyer.shareholder.Id, cost);
    }


    bool HasCompanyOnMarket(GameEntity group, NicheType nicheType)
    {
        return CompanyUtils.HasCompanyOnMarket(group, nicheType, gameContext);
    }
}

using System.Collections.Generic;
using System.Linq;
using Assets.Utils;
using UnityEngine;

public partial class AIManageGroupSystems : OnQuarterChange
{
    void FillUnoccupiedMarkets(GameEntity managingCompany)
    {
        //Debug.Log("Fill Unoccupied Markets: " + managingCompany.company.Name);
        
        foreach (var n in GetUnoccupiedNiches(managingCompany))
            OccupyNiche(n, managingCompany);
    }

    void OccupyNiche(NicheType n, GameEntity managingCompany)
    {
        var products = NicheUtils.GetProductsAvailableForSaleOnMarket(n, gameContext);

        var candidates = GetAcquisitionCandidates(products, managingCompany);

        //Debug.Log("Check unoccupied niche " + n.ToString() + ". Candidates: " + candidates.Count());

        if (candidates.Count() > 0)
            SendAcquisitionOffer(managingCompany, candidates.First(), gameContext);
            //BuyCompany(managingCompany, candidates.First());
    }

    IEnumerable<NicheType> GetUnoccupiedNiches(GameEntity managingCompany)
    {
        var niches = managingCompany.companyFocus.Niches;

        return niches.Where(n => !HasCompanyOnMarket(managingCompany, n));
    }

    IOrderedEnumerable<GameEntity> GetAcquisitionCandidates(GameEntity[] products, GameEntity managingCompany)
    {
        return products
                .Where(p => CompanyUtils.IsWillBuyCompany(managingCompany, p, gameContext))
                .OrderByDescending(p => GetCompanyAcquisitionPriority(managingCompany, p, gameContext));
    }

    long GetCompanyAcquisitionPriority(GameEntity buyer, GameEntity target, GameContext gameContext)
    {
        var price = CompanyEconomyUtils.GetCompanySellingPrice(gameContext, target.company.Id);
        var desireToBuy = CompanyUtils.GetDesireToBuy(buyer, target, gameContext);

        var modifiers = Random.Range(10, 14);

        var priority = modifiers * desireToBuy / (10 * (price + 1));

        //Debug.Log($"Priority of {target.company.Name} in {buyer.company.Name}'s eyes: {priority}");

        return priority;
    }

    void SendAcquisitionOffer(GameEntity buyer, GameEntity target, GameContext gameContext)
    {
        var cost = CompanyEconomyUtils.GetCompanyCost(gameContext, target.company.Id) * Random.Range(1, 10) / 2;

        if (!CompanyUtils.IsEnoughResources(buyer, new Assets.Classes.TeamResource(cost)))
            return;

        CompanyUtils.AddAcquisitionOffer(gameContext, target.company.Id, buyer.shareholder.Id, cost);

        // DON'T TOUCH! this prevents AI from automatically accepting acquisition offer
        if (CompanyUtils.IsCompanyRelatedToPlayer(gameContext, target))
            return;

        // Accept offer
        BuyCompany(buyer, target);

        CompanyUtils.RemoveAcquisitionOffer(gameContext, target.company.Id, buyer.shareholder.Id);
    }

    void BuyCompany(GameEntity managingCompany, GameEntity candidate)
    {
        Debug.LogFormat("{0} wants to buy company {1} ...", managingCompany.company.Name, candidate.company.Name);

        CompanyUtils.BuyCompany(gameContext, candidate.company.Id, managingCompany.shareholder.Id);
    }


    bool HasCompanyOnMarket(GameEntity group, NicheType nicheType)
    {
        return CompanyUtils.HasCompanyOnMarket(group, nicheType, gameContext);
    }
}

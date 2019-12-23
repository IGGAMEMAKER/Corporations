using System.Collections.Generic;
using System.Linq;
using Assets.Utils;
using UnityEngine;

public partial class AIGroupExpansionSystem : OnQuarterChange
{
    void FillUnoccupiedMarkets(GameEntity managingCompany)
    {
        // try to improve influence
        foreach (var n in GetMainMarkets(managingCompany))
            TryToAcquireCompany(n, managingCompany);

        foreach (var n in GetUnoccupiedNiches(managingCompany))
            OccupyNiche(n, managingCompany);
    }

    bool TryToAcquireCompany(NicheType n, GameEntity managingCompany)
    {
        var products = Markets.GetProductsAvailableForSaleOnMarket(n, gameContext);

        var candidates = GetAcquisitionCandidates(products, managingCompany);

        var count = candidates.Count();
        Debug.Log("Check niche " + n + ". Candidates: " + count);

        foreach (var c in candidates)
            SendAcquisitionOffer(managingCompany, c, gameContext);

        return count > 0;
    }

    void OccupyNiche(NicheType n, GameEntity managingCompany)
    {
        var hasCandidates = TryToAcquireCompany(n, managingCompany);

        if (!hasCandidates)
            CreateCompanyOnMarket(n, managingCompany);
    }

    void CreateCompanyOnMarket(NicheType n, GameEntity managingCompany)
    {
        Companies.CreateProductAndAttachItToGroup(gameContext, n, managingCompany);
    }

    IEnumerable<NicheType> GetUnoccupiedNiches(GameEntity managingCompany)
    {
        var niches = managingCompany.companyFocus.Niches;

        return niches.Where(n => !HasCompanyOnMarket(managingCompany, n));
    }

    IEnumerable<NicheType> GetMainMarkets(GameEntity managingCompany)
    {
        var niches = managingCompany.companyFocus.Niches;

        return niches.Where(n => HasCompanyOnMarket(managingCompany, n));
    }

    IEnumerable<GameEntity> GetAcquisitionCandidates(GameEntity[] products, GameEntity managingCompany)
    {
        var culture = Companies.GetOwnCorporateCulture(managingCompany);

        var desireToBuy = 5 - Companies.GetPolicyValue(managingCompany, CorporatePolicy.BuyOrCreate);
        var AIwantsToBuyPlayerCompany = Random.Range(0, 100) < 5 + desireToBuy;

        return products
            .Where(p =>
            p.isIndependentCompany ||
            (Companies.IsCompanyRelatedToPlayer(gameContext, p) && (p.isOnSales || AIwantsToBuyPlayerCompany))
            )
            //.Where(p => Companies.GetFounderAmbition(p, gameContext) == Ambition.EarnMoney)
            ;
                //.Where(p => CompanyUtils.IsWillBuyCompany(managingCompany, p, gameContext))
                //.OrderByDescending(p => GetCompanyAcquisitionPriority(managingCompany, p, gameContext));
    }

    long GetCompanyAcquisitionPriority(GameEntity buyer, GameEntity target, GameContext gameContext)
    {
        var price = Economy.GetCompanySellingPrice(gameContext, target.company.Id);
        var desireToBuy = Companies.GetDesireToBuy(buyer, target, gameContext);

        var modifiers = Random.Range(10, 14);

        var priority = modifiers * desireToBuy / (10 * (price + 1));

        //Debug.Log($"Priority of {target.company.Name} in {buyer.company.Name}'s eyes: {priority}");

        return priority;
    }

    void SendAcquisitionOffer(GameEntity buyer, GameEntity target, GameContext gameContext)
    {
        var cost = Economy.GetCompanyCost(gameContext, target.company.Id) * Random.Range(1, 10) / 2;

        if (!Companies.IsEnoughResources(buyer, new Assets.Classes.TeamResource(cost)))
            return;

        Debug.Log("AI.SendAcquisitionOffer: " + buyer.company.Name + " wants " + target.company.Name);

        Companies.SendAcquisitionOffer(gameContext, target.company.Id, buyer.shareholder.Id, cost);
    }


    bool HasCompanyOnMarket(GameEntity group, NicheType nicheType)
    {
        return Companies.HasCompanyOnMarket(group, nicheType, gameContext);
    }
}

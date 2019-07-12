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
        Debug.Log("Fill Unoccupied Markets: " + managingCompany.company.Name);
        
        foreach (var n in GetUnoccupiedNiches(managingCompany))
            OccupyNiche(n, managingCompany);
    }

    void OccupyNiche(NicheType n, GameEntity managingCompany)
    {
        var products = NicheUtils.GetPlayersOnMarket(gameContext, n).ToArray();

        var candidates = GetAcquisitionCandidates(products, managingCompany);

        Debug.Log("Check unoccupied niche " + n.ToString() + ". Candidates: " + candidates.Count());

        if (candidates.Count() == 0)
            CreateCompany(managingCompany, n);
        else
            BuyCompany(managingCompany, candidates.First());
    }

    IEnumerable<NicheType> GetUnoccupiedNiches(GameEntity managingCompany)
    {
        var niches = managingCompany.companyFocus.Niches;

        return niches.Where(n => !HasCompanyOnMarket(managingCompany, n));
    }

    IOrderedEnumerable<GameEntity> GetAcquisitionCandidates(GameEntity[] products, GameEntity managingCompany)
    {
        return products
                .Where(p => CompanyUtils.IsWillSellCompany(p, gameContext))
                .Where(p => CompanyUtils.IsWillBuyCompany(managingCompany, p))
                .OrderByDescending(p => GetCompanySellingPriority(managingCompany, p, gameContext));
    }

    long GetCompanySellingPriority(GameEntity buyer, GameEntity target, GameContext gameContext)
    {
        return Random.Range(10, 14) * CompanyEconomyUtils.GetCompanySellingPrice(gameContext, target.company.Id) / CompanyUtils.GetDesireToBuy(buyer, target) / 10;
    }

    void BuyCompany(GameEntity managingCompany, GameEntity candidate)
    {
        Debug.LogFormat("{0} wants to buy company {1} ...", managingCompany.company.Name, candidate.company.Name);

        CompanyUtils.BuyCompany(gameContext, candidate.company.Id, managingCompany.shareholder.Id);
    }

    private void CreateCompany(GameEntity managingCompany, NicheType n)
    {
        if (NicheUtils.GetNicheEntity(gameContext, n).nicheState.Phase != NicheLifecyclePhase.Trending)
            return;

        Debug.Log("CreateCompany on market " + n);
        var p = CompanyUtils.AutoGenerateProductCompany(n, gameContext);

        CompanyUtils.AttachToGroup(gameContext, managingCompany.company.Id, p.company.Id);
    }

    bool HasCompanyOnMarket(GameEntity group, NicheType nicheType)
    {
        return CompanyUtils.HasCompanyOnMarket(group, nicheType, gameContext);
    }
}

﻿using System.Collections.Generic;
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
        var niches = managingCompany.companyFocus.Niches;
        var unoccupiedNiches = niches.Where(n => !HasCompanyOnMarket(managingCompany, n));

        foreach (var n in unoccupiedNiches)
        {
            Debug.Log("Check unoccupied niche " + n.ToString());

            var products = NicheUtils.GetPlayersOnMarket(gameContext, n).ToArray();

            var candidates = products
                .Where(p => CompanyUtils.IsWillSellCompany(p, gameContext))
                .Where(p => CompanyUtils.IsWillBuyCompany(managingCompany, p))
                .OrderByDescending(p => GetCompanySellingPriority(managingCompany, p, gameContext));

            Debug.Log("Candidates: " + candidates.Count());

            if (candidates.Count() == 0)
                CreateCompany(managingCompany, n);
            else
                BuyCompany(managingCompany, candidates.First());
        }
    }

    long GetCompanySellingPriority(GameEntity buyer, GameEntity target, GameContext gameContext)
    {
        return Random.Range(10, 14) * CompanyEconomyUtils.GetCompanySellingPrice(gameContext, target.company.Id) / CompanyUtils.GetDesireToBuy(buyer, target) / 10;
    }

    void BuyCompany(GameEntity managingCompany, GameEntity candidate)
    {
        Debug.LogFormat("{0} wants to buy company {1} ...", managingCompany.company.Name, candidate.company.Name);

        var salePrice = CompanyEconomyUtils.GetCompanySellingPrice(gameContext, candidate.company.Id);

        CompanyUtils.BuyCompany(gameContext, candidate.company.Id, managingCompany.shareholder.Id, salePrice);
    }

    private void CreateCompany(GameEntity managingCompany, NicheType n)
    {
        Debug.Log("CreateCompany on market");
        return;
        var p = CompanyUtils.AutoGenerateProductCompany(n, gameContext);

        CompanyUtils.AttachToGroup(gameContext, managingCompany.company.Id, p.company.Id);
    }

    bool HasCompanyOnMarket(GameEntity group, NicheType nicheType)
    {
        return CompanyUtils.HasCompanyOnMarket(group, nicheType, gameContext);
    }

    bool IsCanAffordCompany(GameEntity group, NicheType nicheType)
    {
        return true;
    }
}

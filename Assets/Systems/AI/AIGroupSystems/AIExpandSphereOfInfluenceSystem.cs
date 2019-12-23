using System.Collections.Generic;
using Assets.Utils;
using System.Linq;
using UnityEngine;

public partial class AIGroupExpansionSystem
{
    public void ExpandSphereOfInfluence(GameEntity group)
    {
        CheckNiches(group);
    }

    void CheckNiches(GameEntity group)
    {
        if (IsCompanyReadyToExpand(group))
            AddRandomNiche(group);
    }

    bool IsCompanyReadyToExpand(GameEntity group)
    {
        foreach (var n in group.companyFocus.Niches)
        {
            //Debug.Log("Checking niche " + n.ToString());
            foreach (var holding in Companies.GetDaughterCompanies(gameContext, group.company.Id))
            {
                //Debug.Log("Checking holding " + holding.company.Name);

                if (!holding.hasProduct)
                    continue;

                if (Economy.IsCompanyNeedsMoreMoneyOnMarket(gameContext, holding))
                    return false;
            }
        }

        return true;
    }

    void AddRandomNiche(GameEntity group)
    {
        //Debug.Log("Search Suitable Niche: " + group.company.Name);

        var industry = group.companyFocus.Industries[0];

        var playableNiches = Markets.GetPlayableNichesInIndustry(industry, gameContext);
        var profit = Economy.GetProfit(group, gameContext);

        var averageProfit = profit / (Companies.GetDaughterCompanies(gameContext, group.company.Id).Count() + 1);

        var suitableNiches = playableNiches
            // can start business and hold for a while
            .Where(n => Companies.IsEnoughResources(group, Markets.GetStartCapital(n, gameContext)))
            // exclude niches, that we cover already
            .Where(n => !Companies.IsInSphereOfInterest(group, n.niche.NicheType))
            // is matching our size
            .Where(n => Markets.GetLowestIncomeOnMarket(gameContext, n) > averageProfit / 2)
            //.Where(n => NicheUtils.GetBiggestIncomeOnMarket(gameContext, n) > averageProfit) // can compete with current products
            .ToArray();

        var count = suitableNiches.Count();

        if (count == 0)
            return;

        var rand = Random.Range(0, count);

        //var niche = RandomEnum<NicheType>.GenerateValue();
        var niche = suitableNiches[rand].niche.NicheType;

        Companies.AddFocusNiche(niche, group, gameContext);
    }
}

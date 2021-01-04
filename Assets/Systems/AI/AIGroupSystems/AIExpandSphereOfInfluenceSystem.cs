using System.Collections.Generic;
using Assets.Core;
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
            foreach (var holding in Companies.GetDaughters(group, gameContext))
            {
                //Debug.Log("Checking holding " + holding.company.Name);

                if (!holding.hasProduct)
                    continue;

                if (Economy.IsCompanyNeedsMoreMoneyOnMarket(gameContext, holding))
                    return false;
            }
        }

        var marketLimit = Companies.GetPrimaryMarketsLimit(group, gameContext);
        var willGetPenalty = group.companyFocus.Niches.Count() + 1 > marketLimit;

        // TODO Tweak corporate culture maybe?
        return !willGetPenalty;
    }

    IEnumerable<GameEntity> GetSuitableMarkets(GameEntity group, IndustryType industry)
    {
        var profit = Economy.GetProfit(gameContext, group);

        var amountOfDaughters = Companies.GetDaughtersAmount(group) + 1;
        var averageProfit = profit / amountOfDaughters;


        return Markets.GetPlayableNichesInIndustry(industry, gameContext)
            // can start business and hold for a while
            .Where(n => Companies.IsEnoughResources(group, Markets.GetStartCapital(n, gameContext)))

            // exclude niches, that we cover already
            .Where(n => !Companies.IsInSphereOfInterest(group, n.niche.NicheType))

            // is matching our size
            .Where(n => Markets.GetLowestIncomeOnMarket(gameContext, n) > averageProfit / 2);
    }

    void AddRandomNiche(GameEntity group)
    {
        foreach (var industry in group.companyFocus.Industries)
        {
            var suitableNiches = GetSuitableMarkets(group, industry).ToArray();


            var count = suitableNiches.Count();

            if (count == 0)
                continue;

            var rand = Random.Range(0, count);

            var niche = suitableNiches[rand].niche.NicheType;

            Companies.AddFocusNiche(group, niche, gameContext);
            break;
        }
    }
}

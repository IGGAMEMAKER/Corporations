using System.Collections.Generic;
using Assets.Utils;
using System.Linq;
using UnityEngine;

public partial class AIManageGroupSystems
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



    bool IsCompanyNeedsMoreMoneyOnMarket(GameEntity product)
    {
        return CompanyEconomyUtils.IsCompanyNeedsMoreMoneyOnMarket(gameContext, product);
    }

    bool IsCompanyReadyToExpand(GameEntity group)
    {
        foreach (var n in group.companyFocus.Niches)
        {
            //Debug.Log("Checking niche " + n.ToString());
            foreach (var holding in CompanyUtils.GetDaughterCompanies(gameContext, group.company.Id))
            {
                //Debug.Log("Checking holding " + holding.company.Name);

                if (!holding.hasProduct)
                    continue;

                if (IsCompanyNeedsMoreMoneyOnMarket(holding))
                    return false;
            }
        }

        return true;
    }

    void AddRandomNiche(GameEntity group)
    {
        //Debug.Log("Search Suitable Niche: " + group.company.Name);

        var industry = group.companyFocus.Industries[0];

        var playableNiches = NicheUtils.GetPlayableNichesInIndustry(industry, gameContext);

        var affordableNiches = playableNiches
            .Where(n => CompanyUtils.IsEnoughResources(group, 10 * NicheUtils.GetStartCapital(n)))
            .Where(n => !CompanyUtils.IsInSphereOfInterest(group, n.niche.NicheType))
            .ToArray();

        var count = affordableNiches.Count();

        if (count == 0)
            return;

        var rand = Random.Range(0, count);

        //var niche = RandomEnum<NicheType>.GenerateValue();
        var niche = affordableNiches[rand].niche.NicheType;

        CompanyUtils.AddFocusNiche(niche, group, gameContext);
    }
}

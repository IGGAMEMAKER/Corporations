using System.Collections.Generic;
using Assets.Utils;
using UnityEngine;
using Entitas;

public class AIExpandSphereOfInfluenceSystem : OnQuarterChange
{
    public AIExpandSphereOfInfluenceSystem(Contexts contexts) : base(contexts) { }

    protected override void Execute(List<GameEntity> entities)
    {
        Debug.Log("AIExpandSphereOfInfluenceSystem");

        foreach (var c in CompanyUtils.GetAIManagingCompanies(gameContext))
            CheckNiches(c);
    }

    bool IsCompanyNeedsMoreMoneyOnMarket(GameEntity Group, GameEntity product, NicheType nicheType)
    {
        return CompanyEconomyUtils.IsCompanyNeedsMoreMoneyOnMarket(gameContext, Group, product, nicheType);
    }

    void CheckNiches(GameEntity group)
    {
        Debug.Log("CheckNiches: " + group.company.Name);

        foreach (var n in group.companyFocus.Niches)
        {
            Debug.Log("Checking niche " + n.ToString());
            foreach (var holding in CompanyUtils.GetDaughterCompanies(gameContext, group.company.Id))
            {
                Debug.Log("Checking holding " + holding.company.Name);

                if (!holding.hasProduct)
                    continue;

                if (IsCompanyNeedsMoreMoneyOnMarket(group, holding, n))
                    return;
            }
        }

        SearchSuitableNiche(group);
    }

    void SearchSuitableNiche(GameEntity group)
    {
        Debug.Log("Search Suitable Niche: " + group.company.Name);

        var industry = group.companyFocus.Industries[0];

        var niche = RandomEnum<NicheType>.GenerateValue(group.companyFocus.Niches);

        CompanyUtils.AddFocusNiche(niche, group, gameContext);
    }
}

using System.Collections.Generic;
using Assets.Utils;

public class AIExpandSphereOfInfluenceSystem : OnQuarterChange
{
    public AIExpandSphereOfInfluenceSystem(Contexts contexts) : base(contexts) { }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var c in CompanyUtils.GetAIManagingCompanies(gameContext))
            CheckNiches(c);
    }

    void CheckNiches(GameEntity group)
    {
        if (IsCompanyReadyToExpand(group))
            AddRandomNiche(group);
    }



    bool IsCompanyNeedsMoreMoneyOnMarket(GameEntity product, NicheType nicheType)
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

                if (IsCompanyNeedsMoreMoneyOnMarket(holding, n))
                    return false;
            }
        }

        return true;
    }

    void AddRandomNiche(GameEntity group)
    {
        //Debug.Log("Search Suitable Niche: " + group.company.Name);

        var industry = group.companyFocus.Industries[0];

        var niche = RandomEnum<NicheType>.GenerateValue();

        CompanyUtils.AddFocusNiche(niche, group, gameContext);
    }
}

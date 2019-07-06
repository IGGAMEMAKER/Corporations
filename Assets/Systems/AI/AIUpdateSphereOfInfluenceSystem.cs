using System.Collections.Generic;
using Assets.Utils;

public class AIUpdateSphereOfInfluenceSystem : OnQuarterChange
{
    public AIUpdateSphereOfInfluenceSystem(Contexts contexts) : base(contexts) { }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var c in CompanyUtils.GetAIManagingCompanies(gameContext))
            CheckNiches(c);
    }

    bool IsCompanyNeedsMoreMoneyOnMarket(GameEntity Group, GameEntity product, NicheType nicheType)
    {
        return CompanyEconomyUtils.IsCompanyNeedsMoreMoneyOnMarket(gameContext, Group, product, nicheType);
    }

    void CheckNiches(GameEntity group)
    {
        bool needsMoreMoney = false;
        foreach (var n in group.companyFocus.Niches)
        {
            foreach (var holding in CompanyUtils.GetDaughterCompanies(gameContext, group.company.Id))
            {
                if (!holding.hasProduct)
                    continue;

                if (IsCompanyNeedsMoreMoneyOnMarket(group, holding, n))
                    needsMoreMoney = true;
            }
        }

        if (needsMoreMoney)
            return;

        SearchSuitableNiche(group);
    }

    void SearchSuitableNiche(GameEntity group)
    {
        var industry = group.companyFocus.Industries[0];

        var niche = RandomEnum<NicheType>.GenerateValue(group.companyFocus.Niches);

        CompanyUtils.AddFocusNiche(niche, group, gameContext);
    }
}

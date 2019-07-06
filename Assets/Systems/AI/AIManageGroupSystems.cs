using System.Collections.Generic;
using Assets.Utils;
using Entitas;

public partial class AIManageGroupSystems : OnQuarterChange
{
    public AIManageGroupSystems(Contexts contexts) : base(contexts) { }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var c in CompanyUtils.GetAIManagingCompanies(gameContext))
            ManageGroup(c);
    }

    void ManageGroup(GameEntity c)
    {
        
    }
}

public partial class AIUpdateSphereOfInfluenceSystem : OnQuarterChange
{
    public AIUpdateSphereOfInfluenceSystem(Contexts contexts) : base(contexts) { }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var c in CompanyUtils.GetAIManagingCompanies(gameContext))
            CheckNiches(c);
    }

    bool IsCompanyNeedsMoreMoneyOnMarket(GameEntity Group, GameEntity product, NicheType nicheType)
    {
        MarketingUtils.SetFinancing(gameContext, product.company.Id, MarketingFinancing.High);

        var profitable = CompanyEconomyUtils.GetTotalBalanceChange(product, gameContext) > 0;

        return !profitable;
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

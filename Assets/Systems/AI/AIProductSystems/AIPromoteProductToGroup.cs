using Assets.Utils;
using System.Collections.Generic;

public partial class AIPromoteProductToGroup : OnMonthChange
{
    public AIPromoteProductToGroup(Contexts contexts) : base(contexts) {
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in CompanyUtils.GetAIProducts(gameContext))
            PromoteToGroupIfPossible(e);
    }

    void PromoteToGroupIfPossible(GameEntity product)
    {
        if (!product.isIndependentCompany)
            return;

        var canGrow = EconomyUtils.GetProfit(product, gameContext) > 1000000;

        var ambitions = HumanUtils.GetFounderAmbition(gameContext, product.cEO.HumanId);
        var wantsToGrow = ambitions != Ambition.RuleProductCompany;

        if (canGrow && wantsToGrow)
            CompanyUtils.PromoteProductCompanyToGroup(gameContext, product.company.Id);
    }
}
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
        var wantsToGrow = CompanyUtils.IsProductWantsToGrow(product, gameContext);
        var canGrow = EconomyUtils.GetProfit(product, gameContext) > 1000000;

        if (canGrow && wantsToGrow)
            CompanyUtils.PromoteProductCompanyToGroup(gameContext, product.company.Id);
    }
}
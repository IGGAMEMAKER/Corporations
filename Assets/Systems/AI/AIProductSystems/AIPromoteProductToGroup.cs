using Assets.Utils;
using System.Collections.Generic;

public partial class AIPromoteProductToGroup : OnMonthChange
{
    public AIPromoteProductToGroup(Contexts contexts) : base(contexts) {
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in Companies.GetAIProducts(gameContext))
            PromoteToGroupIfPossible(e);
    }

    void PromoteToGroupIfPossible(GameEntity product)
    {
        var wantsToGrow = Companies.IsProductWantsToGrow(product, gameContext);
        var canGrow = Economy.GetProfit(product, gameContext) > 1000000;

        if (canGrow && wantsToGrow)
            Companies.PromoteProductCompanyToGroup(gameContext, product.company.Id);
    }
}
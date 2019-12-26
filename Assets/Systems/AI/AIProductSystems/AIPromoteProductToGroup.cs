using Assets.Utils;
using System.Collections.Generic;

public partial class AIPromoteProductToGroupSystem : OnMonthChange
{
    public AIPromoteProductToGroupSystem(Contexts contexts) : base(contexts) {}

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in Companies.GetAIProducts(gameContext))
            PromoteToGroupIfPossible(e);
    }

    void PromoteToGroupIfPossible(GameEntity product)
    {
        var wantsToGrow = Economy.GetProfit(product, gameContext) > 500000;
        //Companies.IsProductWantsToGrow(product, gameContext);

        var isDomineering = Markets.GetPositionOnMarket(gameContext, product) == 0;
        var isProfitable = Economy.IsProfitable(gameContext, product);

        var canGrow = isDomineering && isProfitable; // ;

        if (canGrow && wantsToGrow)
            Companies.PromoteProductCompanyToGroup(gameContext, product.company.Id);
    }
}
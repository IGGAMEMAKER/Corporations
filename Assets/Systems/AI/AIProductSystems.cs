using Assets.Utils;
using Entitas;
using System.Collections.Generic;

public class AIProductSystems : OnDateChange
{
    public AIProductSystems(Contexts contexts) : base(contexts) {}

    GameEntity[] GetAIProducts()
    {
        return gameContext.GetEntities(
            GameMatcher
            .AllOf(GameMatcher.Product)
            .NoneOf(GameMatcher.ControlledByPlayer)
        );
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in GetAIProducts())
            ChooseGoal(e);
    }

    long GetBankruptcyScoring(GameEntity product)
    {
        var change = CompanyEconomyUtils.GetBalanceChange(gameContext, product.company.Id);
        var balance = product.companyResource.Resources.money;

        if (change >= 0)
            return 0;

        var timeToBankruptcy = balance / (-change);

        return Constants.COMPANY_SCORING_BANKRUPTCY / (1 + timeToBankruptcy * timeToBankruptcy);
    }

    long GetBadLoyaltyScoring(GameEntity product)
    {
        var coreLoyalty = MarketingUtils.GetClientLoyalty(gameContext, product.company.Id, UserType.Core);
        var regularsLoyalty = MarketingUtils.GetClientLoyalty(gameContext, product.company.Id, UserType.Regular);

        return (coreLoyalty <= 0 || regularsLoyalty <= 0) ? Constants.COMPANY_SCORING_LOYALTY : 0;
    }

    long GetCompanyGoalScoring(GameEntity product)
    {

    }

    void ChooseGoal(GameEntity product)
    {
        // threats
        var bankruptcy = GetBankruptcyScoring(product);
        var badLoyalty = GetBadLoyaltyScoring(product);

        // company goal
        var companyGoal = GetCompanyGoalScoring(product);
    }
}

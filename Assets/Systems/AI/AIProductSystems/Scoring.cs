using Assets.Utils;

public partial class AIProductSystems : OnDateChange
{
    long GetBankruptcyScoring(GameEntity product)
    {
        var change = CompanyEconomyUtils.GetBalanceChange(gameContext, product.company.Id);
        var balance = product.companyResource.Resources.money;

        if (change >= 0)
            return 0;

        var timeToBankruptcy = balance / (-change);

        return Constants.COMPANY_SCORING_BANKRUPTCY / (1 + timeToBankruptcy * timeToBankruptcy);
    }

    long GetCompanyGoalScoring(GameEntity product)
    {
        return Constants.COMPANY_SCORING_COMPANY_GOAL;
        //var requirements = InvestmentUtils.GetGoalRequirements(product, gameContext);

        var goalCompleted = InvestmentUtils.IsGoalCompleted(product, gameContext);

        return goalCompleted ? 0 : Constants.COMPANY_SCORING_COMPANY_GOAL;
    }

    long GetDevelopmentScoring(GameEntity product)
    {
        return 100;
    }

    long GetBadLoyaltyScoring(GameEntity product)
    {
        var coreLoyalty = MarketingUtils.GetClientLoyalty(gameContext, product.company.Id, UserType.Core);
        var regularsLoyalty = MarketingUtils.GetClientLoyalty(gameContext, product.company.Id, UserType.Regular);

        return (coreLoyalty <= 0 || regularsLoyalty <= 0) ? Constants.COMPANY_SCORING_LOYALTY : 0;
    }
}

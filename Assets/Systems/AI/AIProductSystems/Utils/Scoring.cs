using Assets.Utils;

public partial class AIProductSystems : OnDateChange
{
    long GetBankruptcyUrgency(GameEntity product)
    {
        var change = CompanyEconomyUtils.GetBalanceChange(gameContext, product.company.Id);
        var balance = product.companyResource.Resources.money;

        if (change >= 0)
            return 0;

        var timeToBankruptcy = balance / -change;

        long urgency = 0;

        if (timeToBankruptcy < 4)
            urgency = 0;
        else if (timeToBankruptcy < 7)
            urgency = 5;
        else
            urgency = timeToBankruptcy * timeToBankruptcy;

        return Constants.COMPANY_SCORING_BANKRUPTCY / (1 + urgency);
    }

    long GetCompanyGoalUrgency(GameEntity product)
    {
        return Constants.COMPANY_SCORING_COMPANY_GOAL;
    }

    long GetLoyaltyUrgency(GameEntity product)
    {
        var coreLoyalty = MarketingUtils.GetClientLoyalty(gameContext, product.company.Id, UserType.Core);
        var regularsLoyalty = MarketingUtils.GetClientLoyalty(gameContext, product.company.Id, UserType.Regular);

        return (coreLoyalty <= 0 || regularsLoyalty <= 0) ? Constants.COMPANY_SCORING_LOYALTY : 0;
    }
}

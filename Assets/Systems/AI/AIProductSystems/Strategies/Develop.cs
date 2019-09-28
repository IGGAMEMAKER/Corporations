using Assets.Utils;

public partial class AIProductSystems : OnDateChange
{
    void CompleteCompanyGoal(GameEntity company)
    {
        Operate(company);

        InvestmentUtils.CompleteGoal(company, gameContext, false);

        return;
    }
}

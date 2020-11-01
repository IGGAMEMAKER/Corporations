using Assets.Core;

public class StartInvestmentRoundController : ButtonController
{
    public override void Execute()
    {
        if (MyCompany.companyGoal.Goals.Count > 0)
            Companies.StartInvestmentRound(Q, MyCompany);
        else
        {
            NotificationUtils.AddSimplePopup(Q, "Add goals to get investments");
            NavigateToMainScreen();
        }
    }
}

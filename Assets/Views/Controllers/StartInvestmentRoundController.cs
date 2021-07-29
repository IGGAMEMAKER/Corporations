using Assets.Core;

public class StartInvestmentRoundController : ButtonController
{
    public override void Execute()
    {
        if (MyCompany.companyGoal.Goals.Count > 0)
        {
            var profit = Economy.GetProfit(Q, MyCompany);
            bool cashOverflow = Economy.IsHasCashOverflow(Q, MyCompany); // Economy.BalanceOf(MyCompany) > profit * 2;

            if (profit < 0)
                cashOverflow = false;

            var control = Companies.GetShareSize(Q, MyCompany, Hero);
            bool lowControl = control < 10;

            if (lowControl)
            {
                NotificationUtils.AddSimplePopup(Q, "You cannot sell shares anymore", $"Cause you own less than 10% of company (currently: {control}%)");
                return;
            }

            if (cashOverflow)
            {
                NotificationUtils.AddSimplePopup(Q, "You have too much money already!", "Spend it first");
            }
            else
            {
                Companies.StartInvestmentRound(Q, MyCompany);
            }
        }
        else
        {
            NotificationUtils.AddSimplePopup(Q, "Add missions to get investments");
            NavigateToMainScreen();
            OpenModal("Missions");
        }
    }
}

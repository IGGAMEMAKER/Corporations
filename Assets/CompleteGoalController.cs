using Assets.Utils;

public class CompleteGoalController : ButtonController
{
    public override void Execute()
    {
        var goal = MyCompany.companyGoal.InvestorGoal;

        InvestmentUtils.CompleteGoal(MyCompany, GameContext);

        switch (goal)
        {
            case InvestorGoal.Prototype: UnlockTutorialFunctionality(TutorialFunctionality.CompletedFirstGoal); break;
            case InvestorGoal.FirstUsers: UnlockTutorialFunctionality(TutorialFunctionality.GoalFirstUsers); break;
            case InvestorGoal.BecomeMarketFit: UnlockTutorialFunctionality(TutorialFunctionality.GoalBecomeMarketFit); break;
            case InvestorGoal.BecomeProfitable: UnlockTutorialFunctionality(TutorialFunctionality.GoalBecomeProfitable); break;
            case InvestorGoal.Release: UnlockTutorialFunctionality(TutorialFunctionality.GoalRelease); break;
        }
    }
}

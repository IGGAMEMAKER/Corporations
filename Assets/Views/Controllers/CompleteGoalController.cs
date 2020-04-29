using Assets.Core;

public class CompleteGoalController : ButtonController
{
    public override void Execute()
    {
        var goal = MyCompany.companyGoal.InvestorGoal;

        Investments.CompleteGoal(MyCompany, Q);

        switch (goal)
        {
            case InvestorGoal.Prototype: Unlock(TutorialFunctionality.CompletedFirstGoal); break;
            case InvestorGoal.FirstUsers: Unlock(TutorialFunctionality.GoalFirstUsers); break;
            case InvestorGoal.BecomeMarketFit: Unlock(TutorialFunctionality.GoalBecomeMarketFit); break;
            case InvestorGoal.BecomeProfitable: Unlock(TutorialFunctionality.GoalBecomeProfitable); break;
            case InvestorGoal.Release: Unlock(TutorialFunctionality.GoalRelease); break;
        }
    }

    void Unlock(TutorialFunctionality tutorialFunctionality)
    {
        TutorialUtils.Unlock(Q, tutorialFunctionality);
    }
}

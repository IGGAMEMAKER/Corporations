using Assets.Core;

public class CompleteGoalController : ButtonController
{
    public override void Execute()
    {
        //var goal = MyCompany.companyGoal.InvestorGoal;

        //Investments.CompleteGoal(MyCompany, Q);

        //switch (goal)
        //{
        //    case InvestorGoalType.Prototype: Unlock(TutorialFunctionality.CompletedFirstGoal); break;
        //    case InvestorGoalType.FirstUsers: Unlock(TutorialFunctionality.GoalFirstUsers); break;
        //    case InvestorGoalType.BecomeMarketFit: Unlock(TutorialFunctionality.GoalBecomeMarketFit); break;
        //    case InvestorGoalType.BecomeProfitable: Unlock(TutorialFunctionality.GoalBecomeProfitable); break;
        //    case InvestorGoalType.Release: Unlock(TutorialFunctionality.GoalRelease); break;
        //}
    }

    void Unlock(TutorialFunctionality tutorialFunctionality)
    {
        TutorialUtils.Unlock(Q, tutorialFunctionality);
    }
}

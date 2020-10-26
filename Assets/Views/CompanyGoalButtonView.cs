using Assets.Core;

public class CompanyGoalButtonView : CompanyUpgradeButton
{
    InvestorGoalType InvestorGoal;
    InvestmentGoal Goal => Investments.GetInvestmentGoal(MyCompany, Q, InvestorGoal);

    public void SetEntity(InvestorGoalType goal)
    {
        InvestorGoal = goal;

        ViewRender();
    }

    public override string GetButtonTitle()
    {
        return Goal.GetFormattedName();
    }

    public override string GetBenefits()
    {
        return Goal.GetFormattedRequirements(MyCompany, Q);
    }

    public override bool GetState()
    {
        return true;
    }

    public override string GetHint()
    {
        return "";
    }

    public override void Execute()
    {
        FindObjectOfType<InestmentProposalScreen>().SetGoal(InvestorGoal);
    }
}

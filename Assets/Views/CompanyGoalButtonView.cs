public class CompanyGoalButtonView : CompanyUpgradeButton
{
    InvestorGoalType InvestorGoal;
    InvestmentGoal Goal; // => Investments.GetInvestmentGoal(MyCompany, Q, InvestorGoal);

    public void SetEntity(InvestmentGoal goal)
    {
        Goal = goal;

        ViewRender();
    }

    public override string GetButtonTitle()
    {
        if (Goal == null)
            return "";
        return Goal.GetFormattedName();
    }

    public override string GetBenefits()
    {
        if (Goal == null)
            return "";
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
        FindObjectOfType<InestmentProposalScreen>().SetGoal(Goal);
    }
}

using Assets.Core;

public class RenderMyCompanyGoal : ParameterView
{
    public override string RenderValue()
    {
        return "Goal: " + Investments.GetFormattedInvestorGoal(MyCompany.companyGoal.InvestorGoal);
    }
}


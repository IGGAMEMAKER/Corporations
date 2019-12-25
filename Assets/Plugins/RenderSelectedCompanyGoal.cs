using Assets.Utils;

public class RenderSelectedCompanyGoal : UpgradedParameterView
{
    public override string RenderHint()
    {
        return "";
    }

    public override string RenderValue()
    {
        return Investments.GetFormattedInvestorGoal(SelectedCompany.companyGoal.InvestorGoal);
    }
}

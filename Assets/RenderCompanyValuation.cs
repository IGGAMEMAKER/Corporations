using Assets.Utils;

public class RenderCompanyValuation : UpgradedParameterView
{
    public override string RenderHint()
    {
        return "";
    }

    public override string RenderValue()
    {
        return Format.Money(CompanyEconomyUtils.GetCompanyCost(GameContext, SelectedCompany.company.Id));
    }
}

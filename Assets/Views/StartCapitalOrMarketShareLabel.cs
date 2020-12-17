using Assets.Core;

public class StartCapitalOrMarketShareLabel : UpgradedParameterView
{
    public override string RenderHint()
    {
        return "";
    }

    public override string RenderValue()
    {
        var hasCompany = Companies.HasCompanyOnMarket(MyCompany, SelectedNiche, Q);

        if (hasCompany)
            return "Our market share";
        else
            return "Top Maintenance";
    }
}

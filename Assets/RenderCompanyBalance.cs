using Assets.Utils;

public class RenderCompanyBalance : UpgradedParameterView
{
    public override string RenderHint()
    {
        return "";
    }

    public override string RenderValue()
    {
        return Format.Minify(SelectedCompany.companyResource.Resources.money);
    }
}

using Assets.Core;

public class RenderCompanyBalance : UpgradedParameterView
{
    public override string RenderHint()
    {
        return "";
    }

    public override string RenderValue()
    {
        return Format.Minify(Economy.BalanceOf(SelectedCompany));
    }
}

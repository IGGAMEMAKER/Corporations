using Assets.Core;

public class RenderGroupBalance : UpgradedParameterView
{
    public override string RenderHint()
    {
        return "";
    }

    public override string RenderValue()
    {
        return Format.Minify(MyCompany.companyResource.Resources.money);
    }
}

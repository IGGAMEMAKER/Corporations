using Assets.Core;

public class RenderMonthlyGrowth : UpgradedParameterView
{
    public override string RenderHint()
    {
        return growth.ToString();
    }

    public override string RenderValue()
    {
        return $"{Format.Minify(growth)} users";
    }

    long growth => Marketing.GetAudienceGrowth(SelectedCompany, Q);
}

using Assets.Core;

public class RenderMonthlyGrowth : UpgradedParameterView
{
    public override string RenderHint()
    {
        return Marketing.GetAudienceGrowth(SelectedCompany, Q).ToString();
    }

    public override string RenderValue()
    {
        var growth = Marketing.GetAudienceGrowth(SelectedCompany, Q);

        return $"{Format.Minify(growth)} users";
    }
}

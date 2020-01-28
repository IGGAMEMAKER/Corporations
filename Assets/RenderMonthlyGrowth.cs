using Assets.Core;

public class RenderMonthlyGrowth : UpgradedParameterView
{
    public override string RenderHint()
    {
        return Marketing.GetAudienceGrowth(SelectedCompany, GameContext).ToString();
    }

    public override string RenderValue()
    {
        var growth = Marketing.GetAudienceGrowth(SelectedCompany, GameContext);

        return $"{Format.Minify(growth)} users";
    }
}

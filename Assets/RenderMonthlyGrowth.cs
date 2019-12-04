using Assets.Utils;

public class RenderMonthlyGrowth : UpgradedParameterView
{
    public override string RenderHint()
    {
        return MarketingUtils.GetAudienceGrowth(SelectedCompany, GameContext).ToString();
    }

    public override string RenderValue()
    {
        var growth = MarketingUtils.GetAudienceGrowth(SelectedCompany, GameContext);

        return $"{Format.Minify(growth)} users";
    }
}

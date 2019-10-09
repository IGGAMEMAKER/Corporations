using Assets.Utils;

public class ShowImprovementList : UpgradedParameterView
{
    public override string RenderHint()
    {
        return "";
    }

    public override string RenderValue()
    {
        var clients = MarketingUtils.GetAudienceGrowth(SelectedCompany, GameContext);

        var text = "Audience grows by " + Format.Minify(clients) + " clients each month due to current brand power and concept level";

        return text;
    }
}

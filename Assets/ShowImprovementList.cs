using Assets.Utils;

public class ShowImprovementList : UpgradedParameterView
{
    public override string RenderHint()
    {
        return "";
    }

    string DescribeImprovement(TeamUpgrade teamUpgrade, string description = "")
    {
        if (!TeamUtils.IsUpgradePicked(SelectedCompany, teamUpgrade))
            return "";

        var separator = "\n* ";

        return separator + ((description.Length > 0) ? description : teamUpgrade.ToString());
    }

    public override string RenderValue()
    {
        var text = "";

        text += DescribeImprovement(TeamUpgrade.DevelopmentPrototype, "Prototype");
        text += DescribeImprovement(TeamUpgrade.DevelopmentPolishedApp, "Is ready for mass usage");
        text += DescribeImprovement(TeamUpgrade.DevelopmentCrossplatform, "Ready on multiple platforms");

        var clients = MarketingUtils.GetAudienceGrowth(SelectedCompany, GameContext);
        text += "\nAudience grows by " + Format.Minify(clients) + " clients each month";

        return text;
    }
}

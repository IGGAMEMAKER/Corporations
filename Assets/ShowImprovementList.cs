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

        //text += "Development";
        text += DescribeImprovement(TeamUpgrade.DevelopmentPrototype, "Prototype");
        text += DescribeImprovement(TeamUpgrade.DevelopmentPolishedApp, "Is ready for mass usage");
        text += DescribeImprovement(TeamUpgrade.DevelopmentCrossplatform, "Ready on multiple platforms");

        //text += "\nMarketing";
        text += DescribeImprovement(TeamUpgrade.MarketingBase, "They grow normally");
        text += DescribeImprovement(TeamUpgrade.MarketingAggressive, "They grow aggressively");
        text += DescribeImprovement(TeamUpgrade.MarketingAllPlatform);

        var clients = MarketingUtils.GetAudienceGrowth(SelectedCompany, GameContext);
        text += "\nAudience grows by " + Format.Minify(clients) + " clients each month";
        
        //text += "\nSupport";
        text += DescribeImprovement(TeamUpgrade.ClientSupport);
        text += DescribeImprovement(TeamUpgrade.ClientSupportImproved);


        return text;
    }
}

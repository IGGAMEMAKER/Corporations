using Assets.Core;

public class RenderGroupAnnualGrowth : UpgradedParameterView
{
    public override string RenderHint() => "";

    public override string RenderValue()
    {
        var current = CompanyStatisticsUtils.GetCurrentAnnualReport(Q);
        var previous = CompanyStatisticsUtils.GetPreviousAnnualReport(Q);

        var investorId = MyCompany.shareholder.Id;

        var currentReport = current.Groups.Find(r => r.ShareholderId == investorId);
        var previousReport = previous.Groups.Find(r => r.ShareholderId == investorId);


        var prevCost = previousReport.Cost + 1;
        var currCost = currentReport.Cost + 1;

        var growthAbsolute = currCost - prevCost;
        var growthRelative = growthAbsolute * 100 / prevCost;

        bool isGrowth = growthAbsolute >= 0;
        var color = growthAbsolute >= 0 ? Colors.COLOR_POSITIVE : Colors.COLOR_NEGATIVE;

        //$"+23% growth (+$102.4M)"
        var relativeGrowthText = Format.Sign(growthRelative); // Visuals.Colorize(, color);

        var absoluteGrowthText = (isGrowth ? "+" : "") + Format.Money(growthAbsolute, true); // Visuals.Colorize(, color);
        var change = Visuals.DescribeValueWithText(growthAbsolute, "growth" , "loss", "");

        // if company younger than 1 year
        if (growthRelative > 20000)
            return absoluteGrowthText;

        return $"{absoluteGrowthText} {change} ({relativeGrowthText}%)";
    }
}

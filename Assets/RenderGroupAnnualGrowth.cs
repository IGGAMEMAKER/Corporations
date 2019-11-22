using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderGroupAnnualGrowth : UpgradedParameterView
{
    public override string RenderHint() => "";

    public override string RenderValue()
    {
        var current = CompanyStatisticsUtils.GetCurrentAnnualReport(GameContext);
        var previous = CompanyStatisticsUtils.GetPreviousAnnualReport(GameContext);

        var investorId = MyCompany.shareholder.Id;

        var currentReport = current.Groups.Find(r => r.ShareholderId == investorId);
        var previousReport = previous.Groups.Find(r => r.ShareholderId == investorId);


        var prevCost = previousReport.Cost + 1;
        var currCost = currentReport.Cost + 1;

        var growthAbsolute = currCost - prevCost;
        var growthRelative = growthAbsolute * 100 / prevCost;

        bool isGrowth = growthAbsolute >= 0;
        var color = growthAbsolute >= 0 ? VisualConstants.COLOR_POSITIVE : VisualConstants.COLOR_NEGATIVE;

        //$"+23% growth (+$102.4M)"
        var relativeGrowthText = Format.Sign(growthRelative); // Visuals.Colorize(, color);

        var absoluteGrowthText = (isGrowth ? "+" : "") + Format.MinifyMoney(growthAbsolute); // Visuals.Colorize(, color);
        var change = Visuals.DescribeValueWithText(growthAbsolute, "growth" , "loss", "");

        return $"{absoluteGrowthText} {change} ({relativeGrowthText}%)";
    }
}

using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrganisationView : View
{
    public TextMeshProUGUI OrganisationValue;
    public TextMeshProUGUI OrganisationGrowth;

    public Transform loadingBar;
    public Transform textPercent;

    public GameObject ExpandTeam;
    public GameObject FireTeam;

    // ----------------------

    public Text TeamStats;
    public Text Advices;

    public override void ViewRender()
    {
        base.ViewRender();

        var product = Flagship;

        var team = product.team.Teams[SelectedTeam];

        var growth = Teams.GetOrganisationChanges(team, product, Q);

        var organisation = product.team.Teams[SelectedTeam].Organisation;

        var change = growth.Sum() / 10f;
        var organisationChange = Format.ShowChange(change) + " weekly";

        OrganisationValue.text = organisation + "";

        OrganisationGrowth.text = Visuals.DescribeValueWithText(growth.Sum(), organisationChange, organisationChange, "---");
        OrganisationGrowth.GetComponent<Hint>().SetHint($"Changes by {Visuals.Colorize(growth.Sum())}: \n" + growth.ToString() + "\n\nThis value is divided by 10");

        loadingBar.GetComponent<Image>().fillAmount = organisation / 100f;
        //textPercent.GetComponent<TextMeshProUGUI>().text = ((int)value).ToString("F0");

        var teamCount = product.team.Teams.Count;

        bool CanExpand = teamCount == 1 && organisation >= 100;

        Draw(ExpandTeam, CanExpand);

        Draw(FireTeam, team.ID != 0);
        // -------------------------



        var ratingGain = Products.GetFeatureRatingGain(product, team, Q);
        var marketingEffeciency = Marketing.GetMarketingTeamEffeciency(Q, product, team);
        var featureCap = Products.GetFeatureRatingCap(product, team, Q);
        var devSpeed = Products.GetBaseIterationTime(Q, product);

        var stats = new StringBuilder()
            .Append("Max feature level: ")
            .AppendLine(Visuals.Positive(featureCap.ToString("0.0lvl (")) + Visuals.Positive(Format.ShowChange(ratingGain) + "lvl)"))

            //.Append("Feature rating gain: ")
            //.AppendLine(Visuals.Positive(Format.ShowChange(ratingGain) + "lvl"))
            
            .Append("Marketing effeciency: ")
            .AppendLine(Visuals.Positive(marketingEffeciency.ToString("0") + "%"))
            
            //.Append("Development speed: ")
            //.AppendLine(Visuals.Positive(devSpeed.ToString("0days")))
            ;

        TeamStats.text = stats.ToString();

        var advices = new StringBuilder();

        if (Teams.IsNeedsToHireRole(product, WorkerRole.ProductManager, team, Q))
            advices.AppendLine("* Hire product manager to boost rating gain and max feature level");

        if (Teams.IsNeedsToHireRole(product, WorkerRole.MarketingLead, team, Q))
            advices.AppendLine("* Hire marketing lead to boost your marketing effeciency");

        if (Teams.IsNeedsToHireRole(product, WorkerRole.TeamLead, team, Q))
            advices.AppendLine("* Hire team lead to boost iteration speed");

        Advices.text = advices.ToString();
    }
}

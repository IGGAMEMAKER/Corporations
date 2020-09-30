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

    public override void ViewRender()
    {
        base.ViewRender();

        var product = Flagship;

        var team = product.team.Teams[SelectedTeam];

        var growth = Teams.GetOrganisationChanges(team, product, Q);

        var organisation = product.team.Teams[SelectedTeam].Organisation;

        var change = growth.Sum() / 10f;
        var organisationChange = Format.ShowChange(change) + " weekly";

        OrganisationValue.text = organisation.ToString("0.0");

        OrganisationGrowth.text = Visuals.DescribeValueWithText(growth.Sum(), organisationChange, organisationChange, "---");
        OrganisationGrowth.GetComponent<Hint>().SetHint($"Changes by {Visuals.Colorize(growth.Sum())}: \n" + growth.ToString() + "\n\nThis value is divided by 10");

        loadingBar.GetComponent<Image>().fillAmount = organisation / 100f;
        //textPercent.GetComponent<TextMeshProUGUI>().text = ((int)value).ToString("F0");

        var teamCount = product.team.Teams.Count;

        bool CanExpand = teamCount == 1 && organisation >= 100;

        Draw(ExpandTeam, CanExpand);

        Draw(FireTeam, team.ID != 0);
    }
}

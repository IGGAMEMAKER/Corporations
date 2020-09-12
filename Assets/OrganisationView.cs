using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrganisationView : View
{
    public TextMeshProUGUI OrganisationValue;
    public TextMeshProUGUI OrganisationGrowth;

    public Transform loadingBar;
    public Transform textPercent;

    public override void ViewRender()
    {
        base.ViewRender();

        var team = Flagship.team.Teams[SelectedTeam];

        var growth = Teams.GetOrganisationChanges(team, SelectedTeam, Flagship, Q);

        var value = Flagship.team.Teams[SelectedTeam].Organisation;

        var organisationChange = Format.Sign(growth.Sum()) + " weekly";

        OrganisationValue.text = value + "";

        OrganisationGrowth.text = Visuals.DescribeValueWithText(growth.Sum(), organisationChange, organisationChange, "---");
        OrganisationGrowth.GetComponent<Hint>().SetHint("Changes by: " + growth.ToString());

        loadingBar.GetComponent<Image>().fillAmount = value / 100f;
        //textPercent.GetComponent<TextMeshProUGUI>().text = ((int)value).ToString("F0");
    }
}

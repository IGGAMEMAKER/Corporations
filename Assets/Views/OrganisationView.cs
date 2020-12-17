using Assets.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrganisationView : View
{
    public TextMeshProUGUI OrganisationValue;
    public TextMeshProUGUI OrganisationGrowth;

    public Transform loadingBar;
    
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
    }
}

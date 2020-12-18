using Assets.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManagerPointsView : View
{
    public TextMeshProUGUI OrganisationValue;
    public TextMeshProUGUI OrganisationGrowth;

    public Transform loadingBar;
    public override void ViewRender()
    {
        base.ViewRender();
        
        var product = Flagship;

        var team = product.team.Teams[SelectedTeam];

        var organisation = 100; //product.team.Teams[SelectedTeam].Organisation;

        OrganisationValue.text = organisation.ToString("0.0");

        loadingBar.GetComponent<Image>().fillAmount = organisation / 100f;
    }

    private void OnEnable()
    {
        ViewRender();
    }
}
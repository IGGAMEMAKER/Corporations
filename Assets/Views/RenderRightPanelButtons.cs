using Assets.Core;
using TMPro;
using UnityEngine;

public class RenderRightPanelButtons : View
{
    public GameObject NewMarkets;
    public GameObject RaiseInvestments;
    public GameObject CorpCulture;
    public GameObject Partnerships;
    public GameObject ChangeFlagship;

    public GameObject ExploreCompanyButton;
    public GameObject StealFeatureButton;
    public GameObject DetailsButton;

    public override void ViewRender()
    {
        base.ViewRender();

        var canChangeFlagship = Companies.GetDaughterProducts(Q, MyCompany).Length > 1;
        
        Draw(ChangeFlagship, false);
        ChangeFlagship.GetComponentInChildren<TextMeshProUGUI>().text = "CHANGE FLAGSHIP COMPANY";
        
        Draw(RaiseInvestments, false);
        Draw(CorpCulture, false);
        Draw(Partnerships, false);
        Draw(NewMarkets, false);
        
        Draw(ExploreCompanyButton, !Companies.IsDirectlyRelatedToPlayer(Q, SelectedCompany));
    }

    public void OnExploreCompany()
    {
        
    }

    public void OnShowProject()
    {
        ScreenUtils.Navigate(Q, ScreenMode.ProjectScreen);
    }

    public void OnStealFeatures()
    {
        
    }
}

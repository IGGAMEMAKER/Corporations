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

    public override void ViewRender()
    {
        base.ViewRender();

        var canChangeFlagship = Companies.GetDaughterProducts(Q, MyCompany).Length > 1;

        ChangeFlagship.SetActive(canChangeFlagship);
        ChangeFlagship.GetComponentInChildren<TextMeshProUGUI>().text = "CHANGE FLAGSHIP COMPANY";
    }
}

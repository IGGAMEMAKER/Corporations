using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

        var canChangeFlagship = Companies.GetDaughterProductCompanies(Q, MyCompany).Length > 1;

        ChangeFlagship.SetActive(canChangeFlagship);
        ChangeFlagship.GetComponentInChildren<TextMeshProUGUI>().text = "CHANGE FLAGSHIP TO\n" + "Company Name";
    }
}

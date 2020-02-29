using Michsky.UI.Frost;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderCompanyTabButtons : View
{
    public GameObject DevTab;
    public GameObject TeamTab;
    public GameObject EconomyTab;
    public GameObject InfoTab;

    public TopPanelManager TopPanelManager;

    public override void ViewRender()
    {
        base.ViewRender();

        var company = SelectedCompany;

        var isProduct = company.hasProduct;

        DevTab.SetActive(isProduct);

        // if was on product tab and then switched to group, open info tab
        var index = TopPanelManager.currentPanelIndex;

        if (index == 5 && !isProduct)
            TopPanelManager.PanelAnim(0);
    }
}

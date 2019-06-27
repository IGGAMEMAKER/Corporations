using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillCompanySearchListView : View
{
    private void OnEnable()
    {
        Render();
    }

    void Render()
    {
        var products = CompanyUtils.GetProductCompanies(GameContext);

        GetComponent<CompanySearchListView>().SetItems(products);
    }
}

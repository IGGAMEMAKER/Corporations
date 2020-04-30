using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductOverview : View
{
    public Text Popularity;
    public LinkToNiche LinkToNiche;

    public override void ViewRender()
    {
        base.ViewRender();

        if (!SelectedCompany.hasProduct)
            return;

        RenderCommonInfo();
    }

    void RenderCommonInfo()
    {
        var position = Markets.GetPositionOnMarket(Q, SelectedCompany) + 1;
        Popularity.text = $"#{position}";
    }
}

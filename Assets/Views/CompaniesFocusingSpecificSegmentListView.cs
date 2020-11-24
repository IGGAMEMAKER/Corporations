using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CompaniesFocusingSpecificSegmentListView : ListView
{
    ProductPositioning positioning;

    public override void SetItem<T>(Transform t, T entity)
    {
        var company = entity as GameEntity;
        var marketShare = Companies.GetMarketShareOfCompanyMultipliedByHundred(company, Q) / 100f;

        var min = 1f;
        var max = 2.75f;

        var scale = min + (max - min) * marketShare;

        t.GetComponent<CompanyViewInSegmentTab>().SetEntity(company, positioning);
        //t.GetComponent<RectTransform>().localScale = new Vector3(scale, scale, 1);
    }

    public void SetSegment(ProductPositioning productPositioning)
    {
        positioning = productPositioning;

        ViewRender();
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var companies = Companies.GetCompetitionInSegment(Flagship, Q, positioning.ID, true);

        SetItems(companies);
    }
}

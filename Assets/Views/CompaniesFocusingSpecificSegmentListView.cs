using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CompaniesFocusingSpecificSegmentListView : ListView
{
    ProductPositioning positioning;
    bool flag = false;

    public override void SetItem<T>(Transform t, T entity)
    {
        var company = entity as GameEntity;

        t.GetComponent<CompanyViewInSegmentTab>().SetEntity(company, positioning);


        //var marketShare = Companies.GetMarketShareOfCompanyMultipliedByHundred(company, Q) / 100f;

        //var min = 1f;
        //var max = 2.75f;

        //var scale = min + (max - min) * marketShare;
        //t.GetComponent<RectTransform>().localScale = new Vector3(scale, scale, 1);
    }

    public void SetSegment(ProductPositioning productPositioning)
    {
        positioning = productPositioning;
        flag = true;

            var companies = Companies.GetCompetitionInSegment(Flagship, Q, positioning.ID, true);

            SetItems(companies);
    }

    //public override void ViewRender()
    //{
    //    base.ViewRender();

    //    if (flag)
    //    {
    //    }
    //}
}

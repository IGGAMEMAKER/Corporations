using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CompaniesFocusingSpecificSegmentListView : ListView
{
    int segmentId;
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        var company = entity as GameEntity;
        var marketShare = Companies.GetMarketShareOfCompanyMultipliedByHundred(company, Q) / 100f;

        var min = 1f;
        var max = 2.75f;

        var scale = min + (max - min) * marketShare;

        t.GetComponent<CompanyViewOnAudienceMap>().SetEntity(company, segmentId);
        t.GetComponent<RectTransform>().localScale = new Vector3(scale, scale, 1);
    }

    public void SetSegment(int segmentId)
    {
        this.segmentId = segmentId;

        ViewRender();
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var companies = Companies.GetCompetitorsOfCompany(Flagship, Q, true)
            .Where(c => Marketing.IsTargetAudience(c, segmentId));

        SetItems(companies);
    }
}

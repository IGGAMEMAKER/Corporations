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
        t.GetComponent<CompanyViewInSegmentTab>().SetEntity(entity as GameEntity, segmentId);
    }

    public void SetSegment(int segmentId)
    {
        var company = Flagship;

        this.segmentId = segmentId;

        var companies = Companies.GetCompetitorsOfCompany(company, Q, true).Where(c => c.productTargetAudience.SegmentId == segmentId);
        SetItems(companies);
    }
}

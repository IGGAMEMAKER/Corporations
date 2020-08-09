using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProductCompaniesFocusListView : ListView
{
    int segmentId = 0;
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        t.GetComponent<CompaniesFocusingSpecificSegmentListView>().SetSegment((int)(object)entity);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        //var audiences = new List<AudienceInfo>() { Marketing.GetAudienceInfos()[segment] };
        var audiences = new List<int>() { segmentId };

        SetItems(audiences);
    }

    public void SetSegment(int segmentId)
    {
        this.segmentId = segmentId;

        ViewRender();
    }
}

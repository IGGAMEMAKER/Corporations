using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProductCompaniesFocusListView : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        t.GetComponent<CompaniesFocusingSpecificSegmentListView>().SetSegment((int)(object)entity);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var segment = Flagship.productTargetAudience.SegmentId;

        //var audiences = new List<AudienceInfo>() { Marketing.GetAudienceInfos()[segment] };
        var audiences = new List<int>() { segment };

        SetItems(audiences);
    }
}

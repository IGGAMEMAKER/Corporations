using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductCompaniesFocusListView : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        t.GetComponent<CompaniesFocusingSpecificSegmentListView>().SetSegment(index);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        SetItems(Marketing.GetAudienceInfos());
    }
}

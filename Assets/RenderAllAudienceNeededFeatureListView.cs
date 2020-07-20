using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderAllAudienceNeededFeatureListView : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        t.GetComponent<RenderAudienceNeededFeatureListView>().SetAudience(entity as AudienceInfo);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var audiences = Marketing.GetAudienceInfos();

        SetItems(audiences);
    }
}

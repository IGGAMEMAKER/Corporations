using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderFullAudiencesListView : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        t.GetComponent<AudiencePreview>().SetEntity((AudienceInfo)(object)entity);
        t.GetComponent<AudienceMapView>().SetEntity((AudienceInfo)(object)entity);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var infos = Marketing.GetAudienceInfos();

        SetItems(infos);
    }
}

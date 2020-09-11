using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RenderFullAudiencesListView : ListView
{
    long totalPotential;
    int count;
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        t.GetComponent<AudiencePreview>().SetEntity((AudienceInfo)(object)entity);
        t.GetComponent<AudienceMapView>().SetEntity((AudienceInfo)(object)entity, index, count);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var infos = Marketing.GetAudienceInfos()
            .OrderBy(a => a.Size);

        totalPotential = infos.Sum(a => a.Size);
        count = infos.Count();

        SetItems(infos);
    }
}

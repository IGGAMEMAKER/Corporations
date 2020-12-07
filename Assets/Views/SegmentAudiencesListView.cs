using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SegmentAudiencesListView : ListView
{
    public override void SetItem<T>(Transform t, T entity)
    {
        t.GetComponent<AudiencePreview>().SetEntity((AudienceInfo)(object)entity, Flagship);
    }

    public void SetAudiences(ProductPositioning positioning)
    {
        var allAudiences = Marketing.GetAudienceInfos();
        var audiences = positioning.Loyalties
            .Select((l, i) => new { l, i })
            .Where(pp => pp.l >= 0)
            .Select(pp => allAudiences[pp.i]);
        
        SetItems(audiences);
    }
}

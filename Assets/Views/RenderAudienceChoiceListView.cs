using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderAudienceChoiceListView : ListView
{
    public override void SetItem<T>(Transform t, T entity)
    {
        t.GetComponent<SegmentPreview>().SetEntity((ProductPositioning)(object)entity);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var positionings = Marketing.GetNichePositionings(Flagship);

        SetItems(positionings);
    }

    public override void OnItemSelected(int ind)
    {
        base.OnItemSelected(ind);

        var positionings = Marketing.GetNichePositionings(Flagship);

        var p = positionings[ind];

        FindObjectOfType<PositioningManagerView>().SetAnotherPositioning(p);
    }
}

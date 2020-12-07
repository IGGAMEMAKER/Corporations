using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    List<ProductPositioning> GetProductPositionings(List<int> Audiences, GameEntity company)
    {
        return Marketing.GetNichePositionings(company)
            .Where(p => Audiences.All(a => p.Loyalties[a] >= 0))
            .ToList();
    }

    List<ProductPositioning> WrapWithCurrentPositioning(GameEntity company, List<ProductPositioning> positionings)
    {
        var pps = positionings;

        var currentPositioning = Marketing.GetPositioning(Flagship);

        pps.RemoveAll(p => p.ID == currentPositioning.ID);
        pps.Insert(0, currentPositioning);

        return pps;
    }

    public void SetExpansionPositionings()
    {
        var currentPositioning = Marketing.GetPositioning(Flagship);
        var positionings = Marketing.GetNichePositionings(Flagship);

        var favouriteSegments = currentPositioning.Loyalties.Select((l, i) => new { l, i }).Where(r => r.l > 0).Select(r => r.i);
        var favouriteCount = favouriteSegments.Count();

        //var biggerSegments = 
    }

    public void SetPositionings(List<int> Audiences)
    {
        var positionings = WrapWithCurrentPositioning(Flagship, GetProductPositionings(Audiences, Flagship));

        SetItems(positionings);
    }

    public override void OnItemSelected(int ind)
    {
        base.OnItemSelected(ind);

        var positionings = Marketing.GetNichePositionings(Flagship);

        var p = positionings[Item.GetComponent<SegmentPreview>().PositioningID];

        foreach (var it in Items)
        {
            it.GetComponent<SegmentPreview>().DeselectSegment();
        }

        Item.GetComponent<SegmentPreview>().ChooseSegment();

        FindObjectOfType<PositioningManagerView>().SetAnotherPositioning(p);
    }
}

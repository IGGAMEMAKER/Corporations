using Assets.Core;
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
        var positioning = Marketing.GetPositioning(Flagship);
        var positionings = Marketing.GetNichePositionings(Flagship);

        var favouriteSegments = positioning.Loyalties.Select((l, i) => new { l, i }).Where(r => r.l >= 0).Select(r => r.i);
        var favouriteCount = favouriteSegments.Count();

        var expansionPositionings = new List<ProductPositioning>(); // positionings.Where(p => p.Loyalties.All((l, i) => l >= 0 && positioning.Loyalties[i] >= 0));

        foreach (var p in positionings)
        {
            bool willNotLosePreviousUsers = true;
            var newFavouriteCount = p.Loyalties.Count(l => l >= 0);
            
            for (var i = 0; i < p.Loyalties.Count; i++)
            {
                bool likesAudience = positioning.Loyalties[i] >= 0;
                bool willHateAudience = p.Loyalties[i] < 0;

                if (likesAudience && willHateAudience)
                {
                    willNotLosePreviousUsers = false;
                }
            }

            if (willNotLosePreviousUsers && newFavouriteCount > favouriteCount)
                expansionPositionings.Add(p);
        }

        SetPositionings(expansionPositionings);
    }

    public void SetPivotPositionings()
    {
        var positioning = Marketing.GetPositioning(Flagship);
        var positionings = Marketing.GetNichePositionings(Flagship);

        var favouriteSegments = positioning.Loyalties.Select((l, i) => new { l, i }).Where(r => r.l >= 0).Select(r => r.i);
        var favouriteCount = favouriteSegments.Count();

        var pivotPositionings = new List<ProductPositioning>(); // positionings.Where(p => p.Loyalties.All((l, i) => l >= 0 && positioning.Loyalties[i] >= 0));

        foreach (var p in positionings)
        {
            bool willNotLosePreviousUsers = true;
            var newFavouriteCount = p.Loyalties.Count(l => l >= 0);

            for (var i = 0; i < p.Loyalties.Count; i++)
            {
                bool likesAudience = positioning.Loyalties[i] >= 0;
                bool willHateAudience = p.Loyalties[i] < 0;

                if (likesAudience && willHateAudience)
                {
                    willNotLosePreviousUsers = false;
                }
            }

            //willNotLosePreviousUsers && 
            if (newFavouriteCount <= favouriteCount)
                pivotPositionings.Add(p);
        }

        SetPositionings(pivotPositionings);
    }

    public void SetPositionings(List<int> Audiences) => SetPositionings(GetProductPositionings(Audiences, Flagship));
    public void SetPositionings(List<ProductPositioning> positionings1)
    {
        var positionings = WrapWithCurrentPositioning(Flagship, positionings1);

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

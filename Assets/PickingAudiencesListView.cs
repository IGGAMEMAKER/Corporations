using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PickingAudiencesListView : ListView
{
    public override void SetItem<T>(Transform t, T entity)
    {
        t.GetComponent<AudiencePreview>().SetEntity((AudienceInfo)(object)entity, Flagship);
        //t.GetComponent<AudiencePickingPreview>().
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var audiences = Marketing.GetAudienceInfos();

        var product = Flagship;

        if (!product.hasProduct)
            return;

        //bool showAudiences = true;
        bool showAudiences = product.isRelease;

        if (showAudiences)
        {
            SetItems(audiences);
        }
        else
        {
            // take primary audience only
            // positioningId will be always less than amount of audiences
            SetItems(audiences.Where(a => a.ID == Marketing.GetCoreAudienceId(product)));
        }

        Teams.UpdateTeamEfficiency(product, Q);
    }

    public override void OnItemSelected(int ind)
    {
        base.OnItemSelected(ind);

        var audience = Item.GetComponent<AudiencePreview>().segmentId;

        FindObjectOfType<PositioningManagerView>().AddAudience(audience);
    }

    public override void OnDeselect(int ind)
    {
        base.OnDeselect(ind);

        var audience = Item.GetComponent<AudiencePreview>().segmentId;

        FindObjectOfType<PositioningManagerView>().RemoveAudience(audience);
    }
}

using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderAudiencesListView : ListView
{
    public AudienceDetailsView AudienceDetailsView;
    public ChooseTargetAudienceButtonController ChooseTargetAudienceButtonController;
    public ProductCompaniesFocusListView ProductCompaniesFocusListView;

    public MarketingChannelsListView MarketingChannelsListView;

    public bool DisableAutomaticAudienceChange = true;

    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        t.GetComponent<SegmentPreview>().SetEntity(entity as AudienceInfo, index);

        if (DisableAutomaticAudienceChange)
            t.GetComponentInChildren<ChooseTargetAudienceButtonController>().enabled = false;
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var audiences = Marketing.GetAudienceInfos();

        SetItems(audiences);
    }

    public override void OnItemSelected(int ind)
    {
        base.OnItemSelected(ind);

        if (AudienceDetailsView != null)
            AudienceDetailsView.SetAudience(ind);

        if (ChooseTargetAudienceButtonController != null)
        {
            ChooseTargetAudienceButtonController.SetSegment(ind);
            Draw(ChooseTargetAudienceButtonController, ind != Flagship.productTargetAudience.SegmentId);
        }

        if (ProductCompaniesFocusListView != null)
            ProductCompaniesFocusListView.SetSegment(ind);

        if (MarketingChannelsListView != null)
        {
            MarketingChannelsListView.SetSegmentId(ind);
        }
    }

    private void OnEnable()
    {
        ViewRender();

        var TA = Flagship.productTargetAudience.SegmentId;

        ChosenIndex = TA;
        OnItemSelected(TA);
    }

    public void ShowValueChanges(List<long> changes)
    {
        for (var i = 0; i < Items.Count; i++)
        {
            Items[i].GetComponent<SegmentPreview>().AnimateChanges(changes[i]);
        }
    }

    public void HideChanges()
    {
        for (var i = 0; i < Items.Count; i++)
        {
            Items[i].GetComponent<SegmentPreview>().HideChanges();
        }
    }
}

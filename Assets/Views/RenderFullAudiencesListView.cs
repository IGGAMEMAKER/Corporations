using Assets.Core;
using UnityEngine;
using UnityEngine.UI;

public class RenderFullAudiencesListView : ListView
{
    int segmentId = 0;
    public Text AudienceDescription;
    public Text PositionongDescription;

    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        t.GetComponent<AudiencePreview>().SetEntity((AudienceInfo)(object)entity);
        //t.GetComponent<AudienceMapView>().SetEntity((AudienceInfo)(object)entity, index, count);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var infos = Marketing.GetAudienceInfos();
            //.OrderBy(a => a.Size);

        SetItems(infos);

        AudienceDescription.text = Marketing.GetAudienceInfos()[segmentId].Name;
        PositionongDescription.text = $"We are making {Markets.GetCompanyPositioningName(Flagship, Q)}";

        FindObjectOfType<CompaniesFocusingSpecificSegmentListView>().SetSegment(segmentId);
    }

    private void OnEnable()
    {
        segmentId = Flagship.productTargetAudience.SegmentId;

        ViewRender();
    }

    public override void OnItemSelected(int ind)
    {
        base.OnItemSelected(ind);

        segmentId = ind;

        ViewRender();
    }
}

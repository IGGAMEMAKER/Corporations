using Assets.Core;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RenderFullAudiencesListView : ListView
{
    int segmentId = 0;
    public Text AudienceDescription;
    public Text PositionongDescription;
    public Text CompaniesInterestedInUsers;

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

        // TODO DUPLICATED FROM AudiencesOnMainScreenListView.ViewRender()
        var audiences = Marketing.GetAudienceInfos();

        bool showAudiences = true;
        //bool showAudiences = Flagship.isRelease;

        if (showAudiences)
        {
            SetItems(audiences);
        }
        else
        {
            // take primary audience only
            var positioning = Flagship.productPositioning.Positioning;

            SetItems(audiences.Where(a => a.ID == positioning));
        }

        //SetItems(infos);

        var segmentName = Marketing.GetAudienceInfos()[segmentId].Name;

        AudienceDescription.text = segmentName;
        PositionongDescription.text = $"We are making {Markets.GetCompanyPositioningName(Flagship, Q)}";

        if (CompaniesInterestedInUsers != null)
            CompaniesInterestedInUsers.text = $"which are interested in {segmentName}";

        FindObjectOfType<CompaniesFocusingSpecificSegmentListView>().SetSegment(segmentId);
    }

    private void OnEnable()
    {
        //segmentId = Flagship.productTargetAudience.SegmentId;
        segmentId = Mathf.Clamp(Flagship.productPositioning.Positioning, 0, Marketing.GetAudienceInfos().Count); // Marketing.GetAudienceInfos().Where(a => a.ID == ).First().ID;

        ViewRender();
    }

    public override void OnItemSelected(int ind)
    {
        base.OnItemSelected(ind);

        segmentId = Item.GetComponent<AudiencePreview>().segmentId;

        ViewRender();
    }
}

using Assets.Core;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AudienceMapView : View
{
    public CompaniesThatAreFocusingSpecificSegmentListView CompaniesListView;
    public Text SegmentName;

    //public Hint AudienceHint;

    public void SetEntity(AudienceInfo info, long index, int count)
    {
        var companies = Companies.GetCompetitorsOf(Flagship, Q, true);

        var max = 2.75f;
        var scale = Mathf.Clamp(max / (count - index), 1f, max);

        transform.localScale = new Vector3(scale, scale, 1);

        SegmentName.text = info.Name;

        CompaniesListView.SetCompanies(companies.ToArray(), info.ID);
    }
}

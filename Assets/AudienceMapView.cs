using Assets.Core;
using System.Linq;
using UnityEngine;

public class AudienceMapView : View
{
    public CompaniesThatAreFocusingSpecificSegmentListView CompaniesListView;
    //public Hint AudienceHint;

    public void SetEntity(AudienceInfo info, long index, int count)
    {
        var companies = Companies.GetCompetitorsOfCompany(Flagship, Q, true);

        var max = 1.35f;
        var scale = Mathf.Clamp(max / (count - index), 0.8f, max);

        transform.localScale = new Vector3(scale, scale, 1);

        CompaniesListView.SetCompanies(companies.ToArray(), info.ID);
    }
}

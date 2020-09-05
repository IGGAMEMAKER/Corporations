using Assets.Core;
using System.Linq;

public class AudienceMapView : View
{
    public CompaniesThatAreFocusingSpecificSegmentListView CompaniesListView;
    //public Hint AudienceHint;

    public void SetEntity(AudienceInfo info)
    {
        var companies = Companies.GetCompetitorsOfCompany(Flagship, Q, true);

        var asrw = "askda2";

        //asrw.Any(char.IsDigit);

        CompaniesListView.SetCompanies(companies.ToArray(), info.ID);
    }
}

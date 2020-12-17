using UnityEngine;
using UnityEngine.UI;

public class SegmentCompetitionManagerView : View
{
    public Text CompetitionLabel;

    public CompaniesFocusingSpecificSegmentListView CompetingCompaniesListView;
    public GameObject CompetingCompanies;

    public GameObject Label1;
    public GameObject Label2;

    private void OnEnable()
    {
        //OnShowCompetitionLabel();
        OnShowCompetingCompaneis();
    }

    public void OnToggleCompetition()
    {
        if (CompetingCompanies.activeSelf)
        {
            OnShowCompetitionLabel();
        }
        else
        {
            OnShowCompetingCompaneis();
        }
    }

    public void OnShowCompetingCompaneis()
    {
        Debug.Log("OnShowCompetingCompanies()");

        Hide(CompetitionLabel);
        Show(CompetingCompanies);
        Show(CompetingCompaniesListView);
        Show(Label1);
        Show(Label2);
    }

    public void OnShowCompetitionLabel()
    {
        Debug.Log("OnShowCompetitionLabel()");

        Show(CompetitionLabel);
        Hide(CompetingCompanies);
        Hide(CompetingCompaniesListView);
        Hide(Label1);
        Hide(Label2);
    }
}

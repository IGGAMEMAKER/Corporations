using Entitas;
using UnityEngine;
using UnityEngine.UI;

public class BusinessScreenView : View
{
    public GameObject Companies;
    public GameObject CompnayPrefab;
    public Text CompanyNameLabel;
    public LinkToProjectView LinkToProjectView;

    void Awake()
    {
        if (myProductEntity == null)
            Debug.Log("no companies controlled");
    }

    void Update()
    {
        Render();
    }

    void RenderSelectedCompanyName()
    {
        CompanyNameLabel.text = SelectedCompany.company.Name;
    }

    GameEntity[] GetDaughterCompanies()
    {
        return GameContext.GetEntities(GameMatcher.Company);
    }

    void RenderDaughterCompanies(GameEntity[] companies, GameObject Container)
    {
        int index = 0;

        foreach (var e in companies)
        {
            Transform c;

            if (index < Container.transform.childCount - 2)
            {
                c = Container.transform.GetChild(index);
            }
            else
            {
                c = Instantiate(CompnayPrefab, Container.transform, false).transform;
                c.SetSiblingIndex(index);
            }

            c.gameObject.GetComponent<CompanyPreviewView>().SetEntity(e);
            index++;
        }
    }

    void Render()
    {
        LinkToProjectView.CompanyId = SelectedCompany.company.Id;

        RenderSelectedCompanyName();

        var companies = GetDaughterCompanies();

        RenderDaughterCompanies(companies, Companies);
    }
}

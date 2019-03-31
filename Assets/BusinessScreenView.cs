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

    void Start()
    {
        LinkToProjectView.CompanyId = SelectedCompany.company.Id;
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

    void RenderDaughterCompanies(GameEntity[] companies)
    {
        int index = 0;

        foreach (var e in companies)
        {
            Transform c;

            if (index < Companies.transform.childCount - 2)
                c = Companies.transform.GetChild(index);
            else
            {
                c = Instantiate(CompnayPrefab, Companies.transform, false).transform;
                c.SetSiblingIndex(index);
            }

            c.gameObject.GetComponent<CompanyPreviewView>().SetEntity(e);
            index++;
        }
    }

    void Render()
    {
        RenderSelectedCompanyName();

        var companies = GetDaughterCompanies();

        RenderDaughterCompanies(companies);
    }
}

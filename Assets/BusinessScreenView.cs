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

        ProvideEnoughInstances(companies, Container);

        foreach (var e in companies)
        {
            Container.transform
                .GetChild(index)
                .GetComponent<CompanyPreviewView>()
                .SetEntity(e);

            index++;
        }
    }

    void RemoveInstances(int amount)
    {
        // Remove useless gameobjects
    }

    void SpawnInstances(int amount, GameObject Container)
    {
        for (var i = 0; i < amount; i++)
            Instantiate(CompnayPrefab, Container.transform, false);
    }

    void ProvideEnoughInstances(GameEntity[] list, GameObject Container)
    {
        int childCount = Container.transform.childCount;

        int listCount = list.Length;

        if (listCount < childCount)
            RemoveInstances(childCount - listCount);
        else
            SpawnInstances(listCount - childCount, Container);
    }

    void Render()
    {
        LinkToProjectView.CompanyId = SelectedCompany.company.Id;

        RenderSelectedCompanyName();

        var companies = GetDaughterCompanies();

        RenderDaughterCompanies(companies, Companies);
    }
}

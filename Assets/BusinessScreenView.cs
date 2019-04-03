using Assets.Utils;
using Entitas;
using System;
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

    GameEntity[] GetOwnings()
    {
        if (!SelectedCompany.hasShareholder)
            return new GameEntity[0];

        var investableCompanies = GameContext.GetEntities(GameMatcher.AllOf(GameMatcher.Company, GameMatcher.Shareholders));

        int shareholderId = SelectedCompany.shareholder.Id;

        return Array.FindAll(investableCompanies, e => e.shareholders.Shareholders.ContainsKey(shareholderId));
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

    void RemoveInstances(int amount, GameObject Container)
    {
        // Remove useless gameobjects
        for (var i = 0; i < amount; i++)
            Destroy(Container.transform.GetChild(Container.transform.childCount - 1).gameObject);
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
            RemoveInstances(childCount - listCount, Container);
        else
            SpawnInstances(listCount - childCount, Container);
    }

    void Render()
    {
        LinkToProjectView.CompanyId = SelectedCompany.company.Id;

        RenderSelectedCompanyName();

        var companies = GetOwnings();

        RenderDaughterCompanies(companies, Companies);
    }
}

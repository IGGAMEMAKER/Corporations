using Assets.Utils;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public enum GroupCompaniesBy
{
    Niche,
    Neighbours,
    Industry,
    Top
}

public class DiplomaticCompanyDropDown : View
{
    public GroupCompaniesBy FilterBy;
    Dropdown Dropdown;

    GameEntity[] gameEntities;
    GameEntity entity;

    void Start()
    {
        Dropdown = GetComponent<Dropdown>();
    }

    void Update()
    {
        UpdateList();
    }

    void UpdateList()
    {
        gameEntities = GetProperList();

        Dropdown.ClearOptions();
        Dropdown.AddOptions(GetCompanyList(gameEntities));
    }

    List<string> GetCompanyList(GameEntity[] entities)
    {
        List<string> list = new List<string>();

        foreach (var e in entities)
            list.Add(GetCompanyName(e));

        return list;
    }

    string GetCompanyName(GameEntity e)
    {
        switch (FilterBy)
        {
            case GroupCompaniesBy.Niche: return $"{e.product.Name} ({e.product.ProductLevel})";
            case GroupCompaniesBy.Neighbours: return $"{e.product.Name} ({e.marketing.Clients})";
            default: return $"{e.product.Name} ({e.marketing.Clients})";
        }
    }


    public GameEntity[] GetCompetitors()
    {
        return CompanyUtils.GetMyCompetitors(GameContext);
    }

    public GameEntity[] GetNeighbours()
    {
        GameEntity[] products = CompanyUtils.GetProductsNotControlledByPlayer(GameContext);

        return Array.FindAll(products, e => e.product.Niche != MyProduct.Niche && e.product.Industry == MyProduct.Industry);
    }

    GameEntity[] GetProperList()
    {
        switch (FilterBy)
        {
            case GroupCompaniesBy.Niche: return GetCompetitors();
            case GroupCompaniesBy.Neighbours: return GetNeighbours();
            default: return GetNeighbours();
        }
    }
}

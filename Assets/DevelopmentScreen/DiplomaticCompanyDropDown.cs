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

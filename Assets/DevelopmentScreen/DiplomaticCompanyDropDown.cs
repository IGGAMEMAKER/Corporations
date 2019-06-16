using Assets.Utils;
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
        var name = e.company.Name;

        switch (FilterBy)
        {
            case GroupCompaniesBy.Niche: return $"{name} ({e.product.ProductLevel})";
            case GroupCompaniesBy.Neighbours: return $"{name} ({MarketingUtils.GetClients(e)})";
            default: return $"{name} ({MarketingUtils.GetClients(e)})";
        }
    }


    public GameEntity[] GetCompetitors()
    {
        return CompanyUtils.GetMyCompetitors(GameContext);
    }

    public GameEntity[] GetNeighbours()
    {
        return CompanyUtils.GetMyNeighbours(GameContext);
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

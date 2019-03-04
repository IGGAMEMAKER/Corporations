using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

        Dropdown.ClearOptions();

        List<string> vs = new List<string>();

        vs.Add("Option 1");
        vs.Add("Option 2");

        //Dropdown.AddOptions(GetProperList());

        //gameEntities = GameContext.GetEntities(GameMatcher.Product)
    }

    List<string> GetCompanyList(GameEntity[] entities)
    {
        List<string> list = new List<string>();

        foreach (var e in entities)
            list.Add(e.product.Name);

        return list;
    }

    List<string> GetProperList()
    {
        switch (FilterBy)
        {
            case GroupCompaniesBy.Niche:
                return GetCompanyList(GetCompetitors());
                break;

            case GroupCompaniesBy.Neighbours:
                return GetCompanyList(GetNeighbours());
                break;

            default:
                return GetCompanyList(GetNeighbours());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

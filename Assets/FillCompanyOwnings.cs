using Entitas;
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class FillCompanyOwnings : View
    ,IMenuListener
    ,IAnyShareholdersListener
    //,IShareholdersListener
{
    bool SortingOrder = false;
    GameEntity ObservableCompany;

    public abstract GameEntity GetObservableCompany();

    void Start()
    {
        ListenMenuChanges(this);

        Render();
    }

    void OnEnable()
    {
        ObservableCompany = GetObservableCompany();

        if (!ObservableCompany.hasAnyShareholdersListener)
            ObservableCompany.AddAnyShareholdersListener(this);

        Render();
    }

    void OnDisable()
    {
        if (ObservableCompany.hasAnyShareholdersListener)
            ObservableCompany.RemoveAnyShareholdersListener(this);
    }

    GameEntity[] GetInvestableCompanies()
    {
        return GameContext.GetEntities(GameMatcher.AllOf(GameMatcher.Company, GameMatcher.Shareholders));
    }

    public void ToggleSortingOrder()
    {
        SortingOrder = !SortingOrder;

        Render();
    }

    GameEntity[] GetOwnings()
    {
        if (!ObservableCompany.hasShareholder)
            return new GameEntity[0];

        var investableCompanies = GetInvestableCompanies();

        int shareholderId = ObservableCompany.shareholder.Id;

        var arr = Array.FindAll(investableCompanies, e => e.shareholders.Shareholders.ContainsKey(shareholderId));

        if (SortingOrder)
            Array.Reverse(arr);

        return arr;
    }

    void Render()
    {
        var ownings = GetOwnings();

        GetComponent<OwningsListView>().SetItems(ownings);
    }

    void IMenuListener.OnMenu(GameEntity entity, ScreenMode screenMode, object data)
    {
        if (screenMode == ScreenMode.BusinessScreen)
            Render();
    }

    void IAnyShareholdersListener.OnAnyShareholders(GameEntity entity, Dictionary<int, int> shareholders)
    {
        Render();

        Debug.Log("FillCompanyOwnings OnAnyShareholders");
    }
}

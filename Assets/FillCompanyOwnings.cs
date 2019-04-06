using Entitas;
using System;
using System.Collections.Generic;

public class FillCompanyOwnings : View
    ,IMenuListener
    ,IAnyShareholdersListener
    //,IShareholdersListener
{
    bool SortingOrder = false;

    void Start()
    {
        ListenMenuChanges(this);

        Render();
    }

    void OnEnable()
    {
        if (!SelectedCompany.hasAnyShareholdersListener)
            SelectedCompany.AddAnyShareholdersListener(this);

        Render();
    }

    void OnDisable()
    {
        if (SelectedCompany.hasAnyShareholdersListener)
            SelectedCompany.RemoveAnyShareholdersListener(this);
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
        if (!SelectedCompany.hasShareholder)
            return new GameEntity[0];

        var investableCompanies = GetInvestableCompanies();

        int shareholderId = SelectedCompany.shareholder.Id;

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
    }
}

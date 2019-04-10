using Assets.Utils;
using Entitas;
using System;
using System.Collections.Generic;

public class FillCompanyOwnings : View
    ,IMenuListener
    ,IAnyShareholdersListener
{
    bool SortingOrder = false;
    GameEntity ObservableCompany;

    void Start()
    {
        ListenMenuChanges(this);
    }

    public void SetObservableCompany()
    {
        var screenMode = MenuUtils.GetMenu(GameContext).menu.ScreenMode;

        ObservableCompany = screenMode == ScreenMode.GroupManagementScreen ? MyGroupEntity : SelectedCompany;
    }

    void OnEnable()
    {
        SetObservableCompany();

        if (!ObservableCompany.hasAnyShareholdersListener)
            ObservableCompany.AddAnyShareholdersListener(this);

        Render();
    }

    void OnDisable()
    {
        if (ObservableCompany.hasAnyShareholdersListener)
            ObservableCompany.RemoveAnyShareholdersListener(this);
    }

    void Render()
    {
        var ownings = GetOwnings();

        GetComponent<OwningsListView>().SetItems(ownings);
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

    GameEntity[] GetInvestableCompanies()
    {
        return GameContext.GetEntities(GameMatcher.AllOf(GameMatcher.Company, GameMatcher.Shareholders));
    }

    void IMenuListener.OnMenu(GameEntity entity, ScreenMode screenMode, object data)
    {
        if (screenMode == ScreenMode.BusinessScreen || screenMode == ScreenMode.GroupManagementScreen)
            Render();
    }

    void IAnyShareholdersListener.OnAnyShareholders(GameEntity entity, Dictionary<int, int> shareholders)
    {
        Render();
    }
}

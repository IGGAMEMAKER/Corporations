using Assets.Utils;
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

    public void SetObservableCompany()
    {
        var screenMode = ScreenUtils.GetMenu(GameContext).menu.ScreenMode;

        ObservableCompany = screenMode == ScreenMode.GroupManagementScreen ? MyGroupEntity : SelectedCompany;
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
        var arr = CompanyUtils.GetDaughterCompanies(GameContext, ObservableCompany.company.Id);

        if (SortingOrder)
            Array.Reverse(arr);

        return arr;
    }

    void IAnyShareholdersListener.OnAnyShareholders(GameEntity entity, Dictionary<int, BlockOfShares> shareholders)
    {
        Render();
    }

    void IMenuListener.OnMenu(GameEntity entity, ScreenMode screenMode, Dictionary<string, object> data)
    {
        if (screenMode == ScreenMode.GroupManagementScreen)
            Render();
    }
}

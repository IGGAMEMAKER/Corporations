using Assets.Classes;
using Assets.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;

public class MenuResourceView : View
    , IMenuListener
    , IProductListener
    , IMarketingListener
    , ICompanyResourceListener
    , IAnyDateListener
{
    public ResourceView MoneyView;
    public ResourceView ProgrammingView;
    public ResourceView MarketingView;
    public ResourceView ManagerView;
    public ResourceView IdeaView;
    public ResourceView ClientView;
    public ResourceView BrandView;

    public GameObject Container;

    void OnEnable()
    {
        ListenMenuChanges(this);

        if (MyProductEntity != null)
        {
            MyProductEntity.AddProductListener(this);
            MyProductEntity.AddMarketingListener(this);
            MyProductEntity.AddCompanyResourceListener(this);

        }
        LazyUpdate(this);

        Render();
    }

    string GetFormattedPeriod()
    {
        int period = CompanyEconomyUtils.GetPeriodDuration();

        if (period == 1)
            return "Everyday";

        if (period == 7)
            return "Each week";

        if (period == 30)
            return "Every month";

        return "Every " + period + " day(s)";
    }

    string GetHintText<T> (T value)
    {
        long val = long.Parse(value.ToString());

        return String.Format("{1} we get: \n\n {0}", VisualUtils.Sign(val), GetFormattedPeriod());
    }

    void Hide()
    {
        Container.SetActive(false);
    }

    TeamResource GetCompanyResourcePeriodChange()
    {
        return CompanyEconomyUtils.GetResourceChange(MyProductEntity, GameContext);
    }

    public void Render()
    {
        if (CurrentScreen == ScreenMode.DevelopmentScreen ||
            CurrentScreen == ScreenMode.TeamScreen ||
            CurrentScreen == ScreenMode.MarketingScreen
            )
            Render(MyProductEntity.companyResource.Resources, GetCompanyResourcePeriodChange(), MyProductEntity.marketing);
        else
            Hide();
    }

    public void Render(TeamResource teamResource, TeamResource resourceMonthChanges, MarketingComponent marketing)
    {
        Container.SetActive(true);

        string hint;

        // resources
        hint = GetHintText(resourceMonthChanges.money);
        MoneyView.SetPrettifiedValue(hint, teamResource.money);

        hint = GetHintText(resourceMonthChanges.programmingPoints);
        ProgrammingView.SetPrettifiedValue(hint, teamResource.programmingPoints);

        hint = GetHintText(resourceMonthChanges.managerPoints);
        ManagerView.SetPrettifiedValue(hint, teamResource.managerPoints);

        hint = GetHintText(resourceMonthChanges.salesPoints);
        MarketingView.SetPrettifiedValue(hint, teamResource.salesPoints);

        hint = GetHintText(resourceMonthChanges.ideaPoints);
        IdeaView.SetPrettifiedValue(hint, teamResource.ideaPoints);


        // audience
        //hint = String.Format(
        //    "We will lose {0} clients this month due to:\n\n Churn rate: {1}%",
        //    audience.GetChurnClients(),
        //    (int) (audience.GetChurnRate() * 100)
        //);

        ClientView.SetPrettifiedValue("Clients", MarketingUtils.GetClients(MyProductEntity));

        BrandView.SetPrettifiedValue("", marketing.BrandPower);
    }

    void ICompanyResourceListener.OnCompanyResource(GameEntity entity, TeamResource resources)
    {
        Render();
    }

    void IProductListener.OnProduct(GameEntity entity, int id, string name, NicheType niche, int productLevel, int improvementPoints, Dictionary<UserType, int> segments)
    {
        Render();
    }

    void IMarketingListener.OnMarketing(GameEntity entity, long brandPower, Dictionary<UserType, long> segments)
    {
        Render();
    }

    void IMenuListener.OnMenu(GameEntity entity, ScreenMode screenMode, Dictionary<string, object> data)
    {
        Render();
    }

    void IAnyDateListener.OnAnyDate(GameEntity entity, int date)
    {
        Render();
    }
}

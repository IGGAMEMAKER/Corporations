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

        Render();
    }

    string GetHintText<T> (T value)
    {
        string valueSigned = "";

        if (long.Parse(value.ToString()) > 0)
            valueSigned = "+" + value.ToString();
        else
            valueSigned = value.ToString();

        return String.Format("Monthly change \n\n {0}", valueSigned);
    }

    void Hide()
    {
        Container.SetActive(false);
    }

    public void Render()
    {
        if (CurrentScreen == ScreenMode.DevelopmentScreen ||
            CurrentScreen == ScreenMode.TeamScreen ||
            CurrentScreen == ScreenMode.MarketingScreen
            )
            Render(MyProductEntity.companyResource.Resources, new TeamResource(), MyProductEntity.marketing);
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

    void IMenuListener.OnMenu(GameEntity entity, ScreenMode screenMode, object data)
    {
        Render();
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
}

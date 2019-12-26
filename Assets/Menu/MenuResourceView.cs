using Assets.Utils;
using Assets.Utils;
using System;
using UnityEngine;

public class MenuResourceView : View
{
    public ResourceView MoneyView;
    public ResourceView ProgrammingView;
    public ResourceView MarketingView;
    public ResourceView ManagerView;
    public ResourceView IdeaView;
    public ResourceView ClientView;
    public ResourceView BrandView;

    public GameObject Container;

    string GetFormattedPeriod()
    {
        //int period = EconomyUtils.GetPeriodDuration();

        //if (period == 1)
        //    return "Everyday";

        //if (period == 7)
        //    return "Each week";

        //if (period == 30)
        //    return "Every month";

        //return "Every " + period + " day(s)";
        return "";
    }

    string GetHintText<T> (T value)
    {
        long val = long.Parse(value.ToString());

        //return String.Format("{1} we get: \n\n {0}", Format.Sign(val), GetFormattedPeriod());
        return "";
    }

    TeamResource GetCompanyResourcePeriodChange()
    {
        return Economy.GetProductCompanyResourceChange(MyCompany, GameContext);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        Render();
    }

    void Hide()
    {
        Container.SetActive(false);
    }

    public void Render()
    {
        Render(MyCompany.companyResource.Resources, GetCompanyResourcePeriodChange());
    }

    public void Render(TeamResource teamResource, TeamResource resourceMonthChanges)
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

        BrandView.SetPrettifiedValue("Brand power makes your ads more effective", MyCompany.branding.BrandPower);
    }
}

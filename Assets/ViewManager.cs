using Assets.Classes;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ScreenMode
{
    TechnologyScreen,
    MarketingScreen,
    ManagementScreen,
    TeamScreen,
    StatsScreen,
    InvesmentsScreen,
    BusinessScreen
}

public class ViewManager : MonoBehaviour
{
    public GameObject MenuResourceViewObject;

    Dictionary<ScreenMode, GameObject> Screens;

    GameObject Title;

    public ViewManager()
    {
        MenuResourceViewObject = GameObject.Find("Resources");

        Screens = new Dictionary<ScreenMode, GameObject>();

        Screens[ScreenMode.MarketingScreen] = GameObject.Find("AdvertScreen");
        Screens[ScreenMode.TechnologyScreen] = GameObject.Find("TechnologyScreen");
        Screens[ScreenMode.ManagementScreen] = GameObject.Find("ManagerScreen");
        Screens[ScreenMode.TeamScreen] = GameObject.Find("TeamScreen");
        Screens[ScreenMode.StatsScreen] = GameObject.Find("StatsScreen");
        Screens[ScreenMode.InvesmentsScreen] = GameObject.Find("EconomyScreen");
        Screens[ScreenMode.BusinessScreen] = GameObject.Find("BusinessScreen");

        EnableScreen(ScreenMode.ManagementScreen);
    }

    string GetScreenTitle (ScreenMode screen)
    {
        switch (screen)
        {
            case ScreenMode.ManagementScreen: return "Management";
            case ScreenMode.MarketingScreen: return "Marketing";
            case ScreenMode.StatsScreen: return "Companies";
            case ScreenMode.TeamScreen: return "Team";
            case ScreenMode.TechnologyScreen: return "Technologies";
            case ScreenMode.InvesmentsScreen: return "Investments";
            case ScreenMode.BusinessScreen: return "Business";

            default: return "WUT?";
        }
    }

    void SetTitle(ScreenMode screen)
    {
        Title = GameObject.Find("ScreenTitle");
        Title.GetComponent<Text>().text = GetScreenTitle(screen);
    }

    internal void HighlightMonthTick()
    {
        GameObject.Find("Date").GetComponentInChildren<TextBlink>().Reset();
    }

    void DisableScreen(ScreenMode screen)
    {
        if (Screens.ContainsKey(screen))
            Screens[screen].SetActive(false);
    }

    void EnableScreen(ScreenMode screen)
    {
        SetTitle(screen);
        DisableAllScreens();

        if (Screens.ContainsKey(screen))
            Screens[screen].SetActive(true);
    }

    void DisableAllScreens()
    {
        foreach (ScreenMode screen in (ScreenMode[])Enum.GetValues(typeof(ScreenMode)))
            DisableScreen(screen);
    }

    public void RedrawResources(TeamResource resources, TeamResource resourceMonthChanges, Audience audience, string formattedDate)
    {
        MenuResourceViewObject
            .GetComponent<MenuResourceView>()
            .Render(resources, resourceMonthChanges, audience, formattedDate);
    }

    public void RedrawAds(List<Advert> adverts)
    {
        Screens[ScreenMode.MarketingScreen]
            .GetComponent<AdvertRenderer>()
            .UpdateList(adverts);
    }

    public void RedrawTeam(Project p)
    {
        Screens[ScreenMode.TeamScreen]
            .GetComponent<TeamScreenRenderer>()
            .RenderTeam(p);
    }

    public void RedrawCompanies(List<Project> projects, int myCompanyId)
    {
        Screens[ScreenMode.StatsScreen]
            .GetComponent<StatsScreenView>()
            .Redraw(projects, myCompanyId);
    }

    public void RenderTeamScreen()
    {
        EnableScreen(ScreenMode.TeamScreen);
    }

    public void RenderBusinessScreen()
    {
        EnableScreen(ScreenMode.BusinessScreen);
    }

    public void RenderMarketingScreen()
    {
        EnableScreen(ScreenMode.MarketingScreen);
    }

    public void RenderTechnologyScreen()
    {
        EnableScreen(ScreenMode.TechnologyScreen);
    }

    public void RenderManagerScreen()
    {
        EnableScreen(ScreenMode.ManagementScreen);
    }

    public void RenderStatsScreen()
    {
        EnableScreen(ScreenMode.StatsScreen);
    }

    public void RenderInvestmentsScreen()
    {
        EnableScreen(ScreenMode.InvesmentsScreen);
    }
}

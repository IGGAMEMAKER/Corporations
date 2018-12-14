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
    StatsScreen
}

public class ViewManager : MonoBehaviour
{
    // resources
    public GameObject MenuResourceViewObject;
    public GameObject Notifier;
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

        EnableScreen(ScreenMode.MarketingScreen);
    }

    void SetTitle(ScreenMode gameState)
    {
        string name;

        switch (gameState)
        {
            case ScreenMode.ManagementScreen: name = "Management"; break;
            case ScreenMode.MarketingScreen: name = "Marketing"; break;
            case ScreenMode.StatsScreen: name = "Companies"; break;
            case ScreenMode.TeamScreen: name = "Team"; break;
            case ScreenMode.TechnologyScreen: name = "Technologies"; break;

            default:
                name = "WUT?";
                break;
        }

        Title = GameObject.Find("ScreenTitle");
        Title.GetComponent<Text>().text = name;
    }

    internal void HighlightMonthTick()
    {
        GameObject.Find("Date").GetComponentInChildren<TextBlink>().Reset();
    }

    void DisableScreen(ScreenMode gameState)
    {
        if (Screens.ContainsKey(gameState))
            Screens[gameState].SetActive(false);
    }

    void EnableScreen(ScreenMode gameState)
    {
        SetTitle(gameState);
        DisableAllScreens();

        if (Screens.ContainsKey(gameState))
            Screens[gameState].SetActive(true);
    }

    void DisableAllScreens()
    {
        foreach (ScreenMode screen in (ScreenMode[])Enum.GetValues(typeof(ScreenMode)))
            DisableScreen(screen);
    }

    public void RedrawResources(TeamResource resources, TeamResource resourceMonthChanges, Audience audience, string formattedDate)
    {
        MenuResourceViewObject.GetComponent<MenuResourceView>()
            .RedrawResources(resources, resourceMonthChanges, audience, formattedDate);
    }

    public void RedrawAds(List<Advert> adverts)
    {
        Screens[ScreenMode.MarketingScreen].GetComponent<AdvertRenderer>()
            .UpdateList(adverts);
    }

    public void RedrawTeam(Project p)
    {
        Screens[ScreenMode.TeamScreen].GetComponent<TeamScreenRenderer>()
            .RenderTeam(p);
    }

    public void RedrawFeatures(List<Feature> features)
    {
        Screens[ScreenMode.TechnologyScreen].GetComponent<TechnologyScreenRenderer>()
            .RenderFeatures(features);
    }

    public void RedrawCompanies(List<Project> projects, int myCompanyId)
    {
        Screens[ScreenMode.StatsScreen].GetComponent<StatsScreenView>()
            .Redraw(projects, myCompanyId);
    }

    public void RenderTeamScreen()
    {
        EnableScreen(ScreenMode.TeamScreen);
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
}

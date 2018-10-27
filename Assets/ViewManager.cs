using Assets.Classes;
using System;
using System.Collections.Generic;
using UnityEngine;

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
    Dictionary<ScreenMode, GameObject> Screens;

    public ViewManager(GameObject menuResourceViewObject)
    {
        MenuResourceViewObject = menuResourceViewObject;
        Screens = new Dictionary<ScreenMode, GameObject>();

        Screens[ScreenMode.MarketingScreen] = GameObject.Find("AdvertScreen");
        Screens[ScreenMode.TechnologyScreen] = GameObject.Find("TechnologyScreen");
        Screens[ScreenMode.ManagementScreen] = GameObject.Find("ManagerScreen");
        Screens[ScreenMode.TeamScreen] = GameObject.Find("TeamScreen");

        EnableScreen(ScreenMode.TeamScreen);
    }

    void DisableScreen(ScreenMode gameState)
    {
        if (Screens.ContainsKey(gameState))
            Screens[gameState].SetActive(false);
    }

    void EnableScreen(ScreenMode gameState)
    {
        DisableAllScreens();

        if (Screens.ContainsKey(gameState))
        {
            Screens[gameState].SetActive(true);
        }
    }

    void DisableAllScreens()
    {
        DisableScreen(ScreenMode.ManagementScreen);
        DisableScreen(ScreenMode.MarketingScreen);
        DisableScreen(ScreenMode.StatsScreen);
        DisableScreen(ScreenMode.TechnologyScreen);
        DisableScreen(ScreenMode.TeamScreen);
    }

    public void RedrawResources(TeamResource resources, Audience audience, string formattedDate)
    {
        MenuResourceViewObject.GetComponent<MenuResourceView>()
            .RedrawResources(resources, audience, formattedDate);
    }

    public void RedrawAds(List<Advert> adverts)
    {
        Screens[ScreenMode.MarketingScreen].GetComponent<AdvertRenderer>()
            .UpdateList(adverts);
    }

    public void RedrawTeam(Team team)
    {
        Screens[ScreenMode.TeamScreen].GetComponent<TeamScreenRenderer>()
            .RenderTeam(team);
    }

    public void RedrawFeatures(List<Feature> features)
    {
        Screens[ScreenMode.TechnologyScreen].GetComponent<TechnologyScreenRenderer>()
            .RenderFeatures(features);
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
}

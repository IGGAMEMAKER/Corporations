using Assets.Classes;
using System;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    TechnologyScreen,
    MarketingScreen,
    ManagementScreen,
    StatsScreen
}

public class ViewManager : MonoBehaviour
{
    // resources
    public GameObject MenuResourceViewObject;

    GameState menu = GameState.TechnologyScreen;

    Dictionary<GameState, GameObject> Screens;

    public ViewManager(GameObject menuResourceViewObject)
    {
        MenuResourceViewObject = menuResourceViewObject;
        Screens = new Dictionary<GameState, GameObject>();

        Screens[GameState.MarketingScreen] = GameObject.Find("AdvertScreen");
        Screens[GameState.TechnologyScreen] = GameObject.Find("TechnologyScreen");
        Screens[GameState.ManagementScreen] = GameObject.Find("ManagerScreen");

        EnableScreen(menu);
    }

    void DisableScreen(GameState gameState)
    {
        if (Screens.ContainsKey(gameState))
            Screens[gameState].SetActive(false);
    }

    void EnableScreen(GameState gameState)
    {
        menu = gameState;
        DisableAllScreens();

        if (Screens.ContainsKey(gameState))
        {
            Screens[gameState].SetActive(true);
        }
    }

    public void RedrawResources(TeamResource resources, Audience audience, string formattedDate)
    {
        MenuResourceView menuView = MenuResourceViewObject.GetComponent<MenuResourceView>();
        menuView.RedrawResources(resources, audience, formattedDate);
    }

    public void RedrawAds(List<Advert> adverts)
    {
        Screens[GameState.MarketingScreen].GetComponent<AdvertRenderer>().UpdateList(adverts);
    }

    public void RedrawFeatures(List<Feature> features)
    {
        Screens[GameState.TechnologyScreen].GetComponent<TechnologyScreenRenderer>().RenderFeatures(features);
    }

    void DisableAllScreens()
    {
        DisableScreen(GameState.ManagementScreen);
        DisableScreen(GameState.MarketingScreen);
        DisableScreen(GameState.StatsScreen);
        DisableScreen(GameState.TechnologyScreen);
    }

    public void RenderMarketingScreen()
    {
        EnableScreen(GameState.MarketingScreen);
    }

    public void RenderTechnologyScreen()
    {
        EnableScreen(GameState.TechnologyScreen);
    }

    public void RenderManagerScreen()
    {
        EnableScreen(GameState.ManagementScreen);
    }
}

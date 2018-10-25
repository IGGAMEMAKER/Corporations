using Assets.Classes;
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

    GameState menu = GameState.MarketingScreen;

    Dictionary<GameState, GameObject> Screens;

    public ViewManager(GameObject menuResourceViewObject)
    {
        MenuResourceViewObject = menuResourceViewObject;
        Screens = new Dictionary<GameState, GameObject>();

        Debug.LogFormat("get screens");

        Screens[GameState.MarketingScreen] = GameObject.Find("AdvertScreen");
        Screens[GameState.TechnologyScreen] = GameObject.Find("TechnologyScreen");
        Screens[GameState.ManagementScreen] = GameObject.Find("ManagerScreen");        
    }

    void DisableScreen(GameState gameState)
    {
        if (Screens.ContainsKey(gameState))
            Screens[gameState].SetActive(false);
    }

    void EnableScreen(GameState gameState)
    {
        DisableAllScreens();
        menu = gameState;

        if (Screens.ContainsKey(gameState))
            Screens[gameState].SetActive(true);
    }

    public void RedrawResources(TeamResource resources, Audience audience, string formattedDate)
    {
        MenuResourceView menuView = MenuResourceViewObject.GetComponent<MenuResourceView>();
        menuView.RedrawResources(resources, audience, formattedDate);
    }

    public void RedrawAds(List<Advert> adverts)
    {
        AdvertRenderer advertRenderer = Screens[GameState.MarketingScreen].GetComponent<AdvertRenderer>();
        advertRenderer.UpdateList(adverts);
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

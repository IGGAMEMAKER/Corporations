using Assets;
using Assets.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    Dictionary<ScreenMode, GameObject> Screens;

    public Text ScreenTitle;

    public GameObject TechnologyScreen;
    public GameObject ProjectScreen;
    public GameObject BusinessScreen;
    public GameObject InvesmentsScreen;
    public GameObject IndustryScreen;
    public GameObject NicheScreen;

    ScreenMode screen;
    object data;

    // Start is called before the first frame update
    void Start()
    {
        Screens = new Dictionary<ScreenMode, GameObject>
        {
            [ScreenMode.DevelopmentScreen] = TechnologyScreen,
            [ScreenMode.ProjectScreen] = ProjectScreen,
            [ScreenMode.BusinessScreen] = BusinessScreen,
            [ScreenMode.InvesmentsScreen] = InvesmentsScreen,
            [ScreenMode.IndustryScreen] = IndustryScreen,
            [ScreenMode.NicheScreen] = NicheScreen
        };

        data = null;
        EnableScreen(ScreenMode.DevelopmentScreen);
    }

    void Update()
    {
        ToggleScreensIfNecessary();
    }

    string GetScreenTitle(ScreenMode screen)
    {
        switch (screen)
        {
            case ScreenMode.BusinessScreen: return "Business";
            case ScreenMode.IndustryScreen: return "Market resarch";
            case ScreenMode.NicheScreen: return "Niche";
            case ScreenMode.ProjectScreen: return "Project";
            case ScreenMode.DevelopmentScreen: return "Development";
            case ScreenMode.InvesmentsScreen: return "Investments";

            default: return "WUT?";
        }
    }

    void SetTitle(ScreenMode screen)
    {
        ScreenTitle.text = GetScreenTitle(screen);
    }

    void DisableScreen(ScreenMode screen)
    {
        if (Screens.ContainsKey(screen))
            Screens[screen].SetActive(false);
    }

    void EnableScreen(ScreenMode newScreen)
    {
        screen = newScreen;

        SetTitle(newScreen);
        DisableAllScreens();

        if (Screens.ContainsKey(newScreen))
        {
            SoundManager.Play(Sound.Hover);
            Screens[newScreen].SetActive(true);
        }
    }

    void DisableAllScreens()
    {
        foreach (ScreenMode screen in (ScreenMode[])Enum.GetValues(typeof(ScreenMode)))
            DisableScreen(screen);
    }

    void ToggleScreensIfNecessary()
    {
        GameEntity e = MenuUtils.GetMenu(Contexts.sharedInstance.game);

        ScreenMode currentScreen = e.menu.ScreenMode;
        object currentData = e.menu.Data;

        bool needsUpdate =
            screen != currentScreen; // ||
            //data != currentData;

        if (needsUpdate)
            EnableScreen(screen);
    }
}

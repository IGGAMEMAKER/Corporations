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

    // Start is called before the first frame update
    void Start()
    {
        Screens = new Dictionary<ScreenMode, GameObject>();

        Screens[ScreenMode.DevelopmentScreen] = TechnologyScreen;
        Screens[ScreenMode.ProjectScreen] = ProjectScreen;
        Screens[ScreenMode.BusinessScreen] = BusinessScreen;
        Screens[ScreenMode.InvesmentsScreen] = InvesmentsScreen;
        Screens[ScreenMode.IndustryScreen] = IndustryScreen;
        Screens[ScreenMode.NicheScreen] = NicheScreen;

        screen = ScreenMode.DevelopmentScreen;
        EnableScreen(screen);
    }

    void Update()
    {
        ToggleScreensIfNecessary();
    }

    string GetScreenTitle(ScreenMode screen)
    {
        switch (screen)
        {
            // global screens
            case ScreenMode.BusinessScreen: return "Business";
            case ScreenMode.IndustryScreen: return "Market resarch";
            case ScreenMode.NicheScreen: return "Niche";

            // project based screens
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

    void EnableScreen(ScreenMode screen)
    {
        SetTitle(screen);
        DisableAllScreens();

        if (Screens.ContainsKey(screen))
        {
            SoundManager.Play(Sound.Hover);
            Screens[screen].SetActive(true);
        }
    }

    void DisableAllScreens()
    {
        foreach (ScreenMode screen in (ScreenMode[])Enum.GetValues(typeof(ScreenMode)))
            DisableScreen(screen);
    }

    void ToggleScreensIfNecessary()
    {
        GameEntity e = MenuUtils.Menu(Contexts.sharedInstance.game);

        ScreenMode currentScreen = e.menu.ScreenMode;

        if (screen != currentScreen)
        {
            screen = currentScreen;
            EnableScreen(screen);
        } 
    }
}

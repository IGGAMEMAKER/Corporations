using Assets;
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

public class MenuController : MonoBehaviour
{
    Dictionary<ScreenMode, GameObject> Screens;

    public Text ScreenTitle;

    public GameObject MarketingScreen;
    public GameObject TechnologyScreen;
    public GameObject ManagementScreen;
    public GameObject TeamScreen;
    public GameObject StatsScreen;
    public GameObject InvesmentsScreen;
    public GameObject BusinessScreen;


    // Start is called before the first frame update
    void Start()
    {
        Screens = new Dictionary<ScreenMode, GameObject>();

        Screens[ScreenMode.MarketingScreen] = MarketingScreen;
        Screens[ScreenMode.TechnologyScreen] = TechnologyScreen;
        Screens[ScreenMode.ManagementScreen] = ManagementScreen;
        Screens[ScreenMode.TeamScreen] = TeamScreen;
        Screens[ScreenMode.StatsScreen] = StatsScreen;
        Screens[ScreenMode.InvesmentsScreen] = InvesmentsScreen;
        Screens[ScreenMode.BusinessScreen] = BusinessScreen;

        EnableScreen(ScreenMode.TechnologyScreen);
    }

    void Update()
    {
        ToggleScreensIfNecessary();
    }

    string GetScreenTitle(ScreenMode screen)
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
        ScreenTitle.text = GetScreenTitle(screen);
    }

    internal void HighlightMonthTick()
    {
        //GameObject.Find("Date").GetComponentInChildren<TextBlink>().Reset();
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
        if (Input.GetKeyDown(KeyCode.Alpha1))
            EnableScreen(ScreenMode.TechnologyScreen);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            EnableScreen(ScreenMode.InvesmentsScreen);

        if (Input.GetKeyDown(KeyCode.Alpha3))
            EnableScreen(ScreenMode.BusinessScreen);

        if (Input.GetKeyDown(KeyCode.Alpha4))
            EnableScreen(ScreenMode.TeamScreen);

        if (Input.GetKeyDown(KeyCode.Alpha5))
            EnableScreen(ScreenMode.StatsScreen);

        if (Input.GetKeyDown(KeyCode.Alpha6))
            EnableScreen(ScreenMode.MarketingScreen);

        if (Input.GetKeyDown(KeyCode.Alpha7))
            EnableScreen(ScreenMode.ManagementScreen);
    }
}

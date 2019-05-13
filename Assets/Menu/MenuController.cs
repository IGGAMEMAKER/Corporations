using Assets;
using Assets.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ScreenMode
{
    DevelopmentScreen = 0,
    MarketingScreen = 1,
    ProjectScreen = 2,
    TeamScreen = 3,
    StatsScreen = 4,
    CharacterScreen = 5,
    GroupManagementScreen = 6,
    InvesmentsScreen = 7,
    InvesmentProposalScreen = 8,
    IndustryScreen = 9,
    NicheScreen = 10,
    EconomyScreen = 11,
}

public class MenuController : MonoBehaviour, IMenuListener
{
    Dictionary<ScreenMode, GameObject> Screens;

    public Text ScreenTitle;

    public GameObject TechnologyScreen;
    public GameObject ProjectScreen;
    public GameObject InvesmentsScreen;
    public GameObject InvesmentProposalScreen;
    public GameObject IndustryScreen;
    public GameObject NicheScreen;
    public GameObject CharacterScreen;
    public GameObject GroupManagementScreen;
    public GameObject TeamScreen;
    public GameObject EconomyScreen;
    public GameObject MarketingScreen;


    void Start()
    {
        Screens = new Dictionary<ScreenMode, GameObject>
        {
            [ScreenMode.DevelopmentScreen] = TechnologyScreen,
            [ScreenMode.ProjectScreen] = ProjectScreen,
            [ScreenMode.InvesmentsScreen] = InvesmentsScreen,
            [ScreenMode.InvesmentProposalScreen] = InvesmentProposalScreen,
            [ScreenMode.IndustryScreen] = IndustryScreen,
            [ScreenMode.NicheScreen] = NicheScreen,
            [ScreenMode.CharacterScreen] = CharacterScreen,
            [ScreenMode.GroupManagementScreen] = GroupManagementScreen,
            [ScreenMode.TeamScreen] = TeamScreen,
            [ScreenMode.EconomyScreen] = EconomyScreen,
            [ScreenMode.MarketingScreen] = MarketingScreen
        };

        DisableAllScreens();
        EnableScreen(ScreenMode.DevelopmentScreen);

        GameEntity e = ScreenUtils.GetMenu(Contexts.sharedInstance.game);

        e.AddMenuListener(this);
    }

    string GetScreenTitle(ScreenMode screen)
    {
        switch (screen)
        {
            case ScreenMode.IndustryScreen: return "Market resarch";
            case ScreenMode.NicheScreen: return "Market";
            case ScreenMode.ProjectScreen: return "Company Overview";
            case ScreenMode.DevelopmentScreen: return "Product Overview";
            case ScreenMode.InvesmentsScreen: return "Investments";
            case ScreenMode.InvesmentProposalScreen: return "Raise money";
            case ScreenMode.CharacterScreen: return "My Stats";
            case ScreenMode.GroupManagementScreen: return "My companies";
            case ScreenMode.TeamScreen: return "Management";
            case ScreenMode.EconomyScreen: return "Finances";
            case ScreenMode.MarketingScreen: return "Marketing";

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

    void IMenuListener.OnMenu(GameEntity entity, ScreenMode screenMode, Dictionary<string, object> data)
    {
        EnableScreen(screenMode);
    }
}

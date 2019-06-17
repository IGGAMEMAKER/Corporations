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
    InvestmentOfferScreen = 11,
    JobOfferScreen = 12,
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
    public GameObject MarketingScreen;
    public GameObject InvestmentOfferScreen;
    public GameObject JobOfferScreen;


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
            [ScreenMode.MarketingScreen] = MarketingScreen,
            [ScreenMode.InvestmentOfferScreen] = InvestmentOfferScreen,
            [ScreenMode.JobOfferScreen] = JobOfferScreen,
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
            case ScreenMode.GroupManagementScreen: return "My companies";

            case ScreenMode.ProjectScreen: return "Company Overview";
            case ScreenMode.DevelopmentScreen: return "Development";
            case ScreenMode.InvesmentsScreen: return "Funding";
            case ScreenMode.InvesmentProposalScreen: return "Potential investments";
            case ScreenMode.CharacterScreen: return "Profile";
            case ScreenMode.TeamScreen: return "Team";
            case ScreenMode.MarketingScreen: return "Marketing";
            case ScreenMode.InvestmentOfferScreen: return "Investment Offer";
            case ScreenMode.JobOfferScreen: return "Job Offer";

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

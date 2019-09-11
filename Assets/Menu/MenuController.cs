using Assets;
using Assets.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ScreenMode
{
    DevelopmentScreen = 0,
    MarketingScreen = 1, // deprecated
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
    CompanyGoalScreen = 13,
    EmployeeScreen = 14,
    BuySharesScreen = 15,
    ManageCompaniesScreen = 16,
    CompanyEconomyScreen = 17,

    MarketExplorationScreen = 18,
    CompanyExplorationScreen = 19,
    LeaderboardScreen = 20,

    ExplorationScreen = 21,
    NicheInfoScreen = 22,
    AcquisitionScreen = 23,
    SalesScreen = 24,
    PotentialCompaniesScreen = 25,
    AnnualReportScreen = 26,

    StartCampaignScreen = 27,
    GroupScreen = 28,
    HoldingScreen = 29,
    CorporationScreen = 30,
    AcquirableCompaniesOnNicheScreen = 31,

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
    public GameObject CompanyGoalScreen;
    public GameObject EmployeeScreen;
    public GameObject ManageCompaniesScreen;
    public GameObject BuySharesScreen;
    public GameObject CompanyEconomyScreen;
    public GameObject MarketExplorationScreen;
    public GameObject CompanyExplorationScreen;
    public GameObject LeaderboardScreen;
    public GameObject ExplorationScreen;
    public GameObject NicheInfoScreen;
    public GameObject AcquisitionScreen;
    public GameObject SalesScreen;
    public GameObject PotentialCompaniesScreen;
    public GameObject AnnualReportScreen;
    public GameObject StartCampaignScreen;
    public GameObject GroupScreen;
    public GameObject HoldingScreen;
    public GameObject CorporationScreen;
    public GameObject AcquirableCompaniesOnNicheScreen;


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
            [ScreenMode.CompanyGoalScreen] = CompanyGoalScreen,
            [ScreenMode.EmployeeScreen] = EmployeeScreen,
            [ScreenMode.ManageCompaniesScreen] = ManageCompaniesScreen,
            [ScreenMode.BuySharesScreen] = BuySharesScreen,
            [ScreenMode.CompanyEconomyScreen] = CompanyEconomyScreen,
            [ScreenMode.MarketExplorationScreen] = MarketExplorationScreen,
            [ScreenMode.CompanyExplorationScreen] = CompanyExplorationScreen,
            [ScreenMode.LeaderboardScreen] = LeaderboardScreen,
            [ScreenMode.ExplorationScreen] = ExplorationScreen,
            [ScreenMode.NicheInfoScreen] = NicheInfoScreen,
            [ScreenMode.AcquisitionScreen] = AcquisitionScreen,
            [ScreenMode.SalesScreen] = SalesScreen,
            [ScreenMode.PotentialCompaniesScreen] = PotentialCompaniesScreen,
            [ScreenMode.AnnualReportScreen] = AnnualReportScreen,
            [ScreenMode.StartCampaignScreen] = StartCampaignScreen,
            [ScreenMode.GroupScreen] = GroupScreen,
            [ScreenMode.HoldingScreen] = HoldingScreen,
            [ScreenMode.CorporationScreen] = CorporationScreen,
            [ScreenMode.AcquirableCompaniesOnNicheScreen] = AcquirableCompaniesOnNicheScreen,
        };

        DisableAllScreens();
        
        EnableScreen(ScreenMode.NicheScreen);

        GameEntity e = ScreenUtils.GetMenu(Contexts.sharedInstance.game);

        e.AddMenuListener(this);
    }

    string GetScreenTitle(ScreenMode screen)
    {
        switch (screen)
        {
            case ScreenMode.IndustryScreen: return "Market resarch";
            case ScreenMode.NicheScreen: return "Market";
            case ScreenMode.GroupManagementScreen: return "My group company";
            case ScreenMode.ManageCompaniesScreen: return "My companies";

            case ScreenMode.ProjectScreen: return "Company Overview";
            case ScreenMode.DevelopmentScreen: return "Development";
            case ScreenMode.MarketingScreen: return "Marketing";

            case ScreenMode.InvesmentsScreen: return "Funding";
            case ScreenMode.InvesmentProposalScreen: return "Potential investments";
            case ScreenMode.InvestmentOfferScreen: return "Investment Offer";

            case ScreenMode.CompanyGoalScreen: return "Company Goal";

            case ScreenMode.CharacterScreen: return "Profile";
            case ScreenMode.TeamScreen: return "Team";
            case ScreenMode.JobOfferScreen: return "Job Offer";
            case ScreenMode.BuySharesScreen: return "Buy Shares";
            case ScreenMode.EmployeeScreen: return "Employees";
            case ScreenMode.CompanyEconomyScreen: return "Economy";

            case ScreenMode.MarketExplorationScreen: return "Explore new markets";
            case ScreenMode.CompanyExplorationScreen: return "Explore companies";
            case ScreenMode.LeaderboardScreen: return "Dorbes list";
            case ScreenMode.ExplorationScreen: return "Explore";
            case ScreenMode.NicheInfoScreen: return "Niche details";

            case ScreenMode.AcquisitionScreen: return "Acquisition";
            case ScreenMode.SalesScreen: return "Sell my shares";
            case ScreenMode.PotentialCompaniesScreen: return "These companies are on sale";
            case ScreenMode.AnnualReportScreen: return "Annual Report";

            case ScreenMode.StartCampaignScreen: return "Start new campaign";
            case ScreenMode.GroupScreen: return "Group";
            case ScreenMode.HoldingScreen: return "Holding";
            case ScreenMode.CorporationScreen: return "Corporation";

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

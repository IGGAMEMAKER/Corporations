﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : View
{
    // prefabs
    Dictionary<ScreenMode, GameObject> Screens;
    
    // string = url
    private Dictionary<ScreenMode, string> PrefabScreens;
    
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
    public GameObject JoinCorporationScreen;
    public GameObject FormStrategicPartnershipScreen;
    public GameObject TrendsScreen;
    public GameObject MessageScreen;

    string GetWrappedUrl(string url)
    {
        return "/" + url;
    }

    void SetUrl(ScreenMode mode, string url)
    {
        PrefabScreens[mode] = GetWrappedUrl(url);
    }
    
    void Start()
    {
        var mockupUrl = GetWrappedUrl("ProjectScreen");
        PrefabScreens = new Dictionary<ScreenMode, string>();

        SetUrl(ScreenMode.DevelopmentScreen, "DevelopmentScreen");
        SetUrl(ScreenMode.ProjectScreen, "ProjectScreen");
        SetUrl(ScreenMode.InvesmentsScreen, "InvestmentScreen");
        SetUrl(ScreenMode.InvesmentProposalScreen, "InvestmentRoundsScreen");
        SetUrl(ScreenMode.IndustryScreen, "IndustryScreen");
        SetUrl(ScreenMode.NicheScreen, "NicheScreen");
        SetUrl(ScreenMode.TeamScreen, "TeamScreen");
        SetUrl(ScreenMode.CharacterScreen, "CharacterScreen");
        
        SetUrl(ScreenMode.GroupManagementScreen, "ProjectScreen");
        
        SetUrl(ScreenMode.MarketingScreen, "MarketingScreen");
        SetUrl(ScreenMode.InvestmentOfferScreen, "InvestmentOfferScreen");
        SetUrl(ScreenMode.JobOfferScreen, "JobOfferScreen");
        SetUrl(ScreenMode.CompanyGoalScreen, "CompanyGoalScreen");
        SetUrl(ScreenMode.EmployeeScreen, "EmployeesScreen");
        SetUrl(ScreenMode.ManageCompaniesScreen, "HierarchyScreen");
        SetUrl(ScreenMode.BuySharesScreen, "BuySharesScreen");
        SetUrl(ScreenMode.CompanyEconomyScreen, "EconomyScreen");
        SetUrl(ScreenMode.MarketExplorationScreen, "MarketResearchScreen");
        SetUrl(ScreenMode.CompanyExplorationScreen, "CompanyResearchScreen");
        SetUrl(ScreenMode.LeaderboardScreen, "LeaderboardScreen");
        SetUrl(ScreenMode.ExplorationScreen, "ExplorationScreen");
        SetUrl(ScreenMode.NicheInfoScreen, "NicheInfoScreen");
        SetUrl(ScreenMode.AcquisitionScreen, "AcquisitionScreen");
        SetUrl(ScreenMode.SalesScreen, "SalesScreen");
        SetUrl(ScreenMode.PotentialCompaniesScreen, "PotentialCompaniesScreen");
        SetUrl(ScreenMode.AnnualReportScreen, "AnnualReportScreen");
        SetUrl(ScreenMode.StartCampaignScreen, "StartCampaignScreen");
        
        SetUrl(ScreenMode.GroupScreen, "ProjectScreen");
        
        SetUrl(ScreenMode.HoldingScreen, "CorporateCultureScreen");
        SetUrl(ScreenMode.CorporationScreen, "CorporationScreen");
        SetUrl(ScreenMode.AcquirableCompaniesOnNicheScreen, "AcquirableCompaniesOnNicheScreen");
        SetUrl(ScreenMode.JoinCorporationScreen, "JoinCorporationScreen");
        SetUrl(ScreenMode.FormStrategicPartnershipScreen, "FormStrategicPartnershipScreen");
        SetUrl(ScreenMode.TrendsScreen, "TrendsScreen");
        SetUrl(ScreenMode.MessageScreen, "MessageScreen");

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
            [ScreenMode.JoinCorporationScreen] = JoinCorporationScreen,
            [ScreenMode.FormStrategicPartnershipScreen] = FormStrategicPartnershipScreen,
            [ScreenMode.TrendsScreen] = TrendsScreen,
            [ScreenMode.MessageScreen] = MessageScreen,
        };

        //DisableAllScreens();
        
        EnableScreen(ScreenMode.HoldingScreen);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        EnableScreen(CurrentScreen);
    }

    void ToggleScreen(ScreenMode screen, bool state)
    {
        try
        {
            if (Screens != null && Screens.ContainsKey(screen))
                Draw(Screens[screen], state);
        }
        catch
        {
            Debug.LogError($"Failed to toggle ({state}) screen: " + screen);
        }
    }

    void DisableScreen(ScreenMode screen)
    {
        ToggleScreen(screen, false);
    }

    void EnableScreen(ScreenMode screen)
    {
        SetTitle(screen);
        // DisableAllScreens();
        OpenUrl(PrefabScreens[screen]);

        // ToggleScreen(screen, true);
    }

    //[ExecuteInEditMode]
    

    void DisableAllScreens()
    {
        foreach (ScreenMode screen in (ScreenMode[])Enum.GetValues(typeof(ScreenMode)))
            DisableScreen(screen);
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
            case ScreenMode.JoinCorporationScreen: return "Join Corporation";

            case ScreenMode.TrendsScreen: return "Trends";

            default: return "WUT?";
        }
    }

    void SetTitle(ScreenMode screen)
    {
        // ScreenTitle.text = GetScreenTitle(screen);
    }
}

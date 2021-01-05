using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : View
{
    // prefabs
    Dictionary<ScreenMode, GameObject> Screens;
    
    // string = url
    public Dictionary<ScreenMode, string> PrefabScreens = new Dictionary<ScreenMode, string>();

    string GetWrappedUrl(string url)
    {
        return "/" + url;
    }

    void SetUrl(ScreenMode mode, string url)
    {
        PrefabScreens[mode] = GetWrappedUrl(url);
    }

    void LoadMenuMappings()
    {
        // PrefabScreens = new Dictionary<ScreenMode, string>();
        if (PrefabScreens.Count > 0)
            return;

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
        SetUrl(ScreenMode.GroupScreen, "CorporateCultureScreen");
        
        SetUrl(ScreenMode.HoldingScreen, "Holding");
        
        SetUrl(ScreenMode.CorporationScreen, "CorporationScreen");
        SetUrl(ScreenMode.AcquirableCompaniesOnNicheScreen, "AcquirableCompaniesOnNicheScreen");
        SetUrl(ScreenMode.JoinCorporationScreen, "JoinCorporationScreen");
        SetUrl(ScreenMode.FormStrategicPartnershipScreen, "FormStrategicPartnershipScreen");
        SetUrl(ScreenMode.TrendsScreen, "TrendsScreen");
        SetUrl(ScreenMode.MessageScreen, "MessageScreen");
    }
    
    // void Start()
    // {
    //     EnableScreen(ScreenMode.HoldingScreen);
    // }

    public override void ViewRender()
    {
        base.ViewRender();

        EnableScreen(CurrentScreen);
    }

    void EnableScreen(ScreenMode screen)
    {
        LoadMenuMappings();
        
        Debug.Log("Enable screen " + screen);

        try
        {
            OpenUrl(PrefabScreens[screen]);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Cannot open {screen}");
        }
    }
}

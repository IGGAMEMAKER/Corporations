using Assets.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CompanyTaskTypeRelay : View
{
    [Header("Counters")]
    public GameObject FeatureCounter;
    public GameObject MarketingCounter;
    public GameObject TeamCounter;
    public GameObject GoalCounter;

    [Header("Buttons")]
    public GameObject MarketingButton;
    public GameObject DevelopmentButton;

    public GameObject InvestmentButton;
    public GameObject ServersButton;
    public GameObject TeamsButton;

    public GameObject MissionButton;
    public GameObject OffersButton;
    public GameObject CompetitorsButton;

    void OnEnable()
    {
        ViewRender();
    }

    public override void ViewRender()
    {
        base.ViewRender();

        RenderFeatureButton();
        RenderMarketingButton();
        RenderServerButton();
        RenderTeamButton();

        RenderInvestmentButton();
        RenderOffersButton();
        RenderMissionsButton();
        RenderCompetitorsButton();
    }

    private void RenderOffersButton()
    {
        Draw(OffersButton, false);
    }

    private void RenderCompetitorsButton()
    {
        Draw(CompetitorsButton, true);
    }

    private void RenderMissionsButton()
    {
        bool hasGoals = Flagship.companyGoal.Goals.Count > 0;
        bool completedGoals = Flagship.completedGoals.Goals.Count > 0;
        Draw(MissionButton, hasGoals || completedGoals);

        bool canCompleteGoals = Investments.IsCanCompleteAnyGoal(Flagship, Q);
        Draw(GoalCounter, hasGoals);
    }

    void RenderFeatureButton()
    {
        var features = Products.GetProductFeaturesList(Flagship, Q).Length;

        Draw(FeatureCounter, features > 0);

        FeatureCounter.GetComponentInChildren<Text>().text = features.ToString();
    }

    void RenderMarketingButton()
    {
        var channels = Markets.GetAffordableMarketingChannels(Flagship, Q).Length;

        Draw(MarketingCounter, channels > 0);

        MarketingCounter.GetComponentInChildren<Text>().text = channels.ToString();
    }

    void RenderInvestmentButton()
    {
        bool willBeBankruptSoon = Economy.IsWillBecomeBankruptOnNextPeriod(Q, Flagship);
        bool skippedSomeTime = CurrentIntDate > 60;
        //bool hadBankruptcyWarning =  || ; // NotificationUtils.GetPopupContainer(Q).seenPopups.PopupTypes.Contains(PopupType.BankruptcyThreat);

        Draw(InvestmentButton, willBeBankruptSoon || skippedSomeTime);
    }

    void RenderServerButton()
    {
        bool hadFirstMarketingCampaign = Marketing.GetUsers(Flagship) > 50;
        bool serverOverload = Products.IsNeedsMoreServers(Flagship);

        //Draw(ServersButton, hadFirstMarketingCampaign || serverOverload);
    }

    void RenderTeamButton()
    {
        int teamInterrupts = Flagship.team.Teams.Count(t => Teams.IsTeamNeedsAttention(Flagship, t, Q));

        Draw(TeamsButton, Flagship.team.Teams.Count > 0);
        Draw(TeamCounter, teamInterrupts > 0);
        TeamCounter.GetComponentInChildren<Text>().text = teamInterrupts.ToString();

        //// 1, cause servers are added automatically
        //bool anyTaskWasAdded = Flagship.team.Teams[0].Tasks.Count > 2;
        //bool hasMoreThanOneTeam = Flagship.team.Teams.Count > 1;

        //if (anyTaskWasAdded || hasMoreThanOneTeam)
        //{
        //    //Show(AddTeamButton);
        //    //Show(TeamPanel);
        //    //Show(TeamLabel);
        //}
    }
}

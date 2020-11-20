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
    public GameObject MonetizationFeatureCounter;
    public GameObject MarketingCounter;
    public GameObject TeamCounter;
    public GameObject GoalCounter;

    [Header("Buttons")]
    public GameObject MarketingButton;
    public GameObject DevelopmentButton;
    public GameObject MonetizationButton;

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
        RenderMonetizationButton();
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
        Draw(CompetitorsButton, false);
    }

    private void RenderMissionsButton()
    {
        var goalsCount = MyCompany.companyGoal.Goals.Count;
        var newGoalsCount = Investments.GetNewGoals(MyCompany, Q).Count;

        bool completedGoals = MyCompany.completedGoals.Goals.Count > 0;
        bool canCompleteGoals = Investments.IsCanCompleteAnyGoal(MyCompany, Q);

        bool hasGoals = goalsCount > 0;
        bool hasNewGoals = newGoalsCount > 0;

        Draw(MissionButton, true); // hasGoals || completedGoals
        MissionButton.GetComponent<Blinker>().enabled = canCompleteGoals;


        //Draw(GoalCounter, hasGoals);
        GoalCounter.GetComponent<Image>().enabled = hasNewGoals;
        GoalCounter.GetComponentInChildren<Image>().enabled = hasNewGoals;

        GoalCounter.GetComponentInChildren<Text>().text = hasGoals ? goalsCount.ToString() : newGoalsCount.ToString();
    }

    void RenderFeatureButton()
    {
        var features = Products.GetProductFeaturesList(Flagship, Q).Length;

        Draw(FeatureCounter, features > 0);

        FeatureCounter.GetComponentInChildren<Text>().text = features.ToString();
    }

    void RenderMonetizationButton()
    {
        var features = Products.GetUpgradeableMonetisationFeatures(Flagship, Q).Length;

        Draw(MonetizationFeatureCounter, features > 0);
        Draw(MonetizationButton, Flagship.isRelease);

        MonetizationFeatureCounter.GetComponentInChildren<Text>().text = features.ToString();
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

        Draw(ServersButton, true);
        //Draw(ServersButton, hadFirstMarketingCampaign || serverOverload);
    }

    void RenderTeamButton()
    {
        int teamInterrupts = Flagship.team.Teams.Count(t => Teams.IsTeamNeedsAttention(Flagship, t, Q));

        Draw(TeamsButton, Flagship.team.Teams.Count > 0);
        Draw(TeamCounter, teamInterrupts > 0);
        TeamCounter.GetComponentInChildren<Text>().text = teamInterrupts.ToString();
    }
}

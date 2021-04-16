using Assets.Core;
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

    void OnEnable()
    {
        ViewRender();
    }

    public override void ViewRender()
    {
        base.ViewRender();

        bool forceShow = true;

        RenderMissionsButton(forceShow);
        RenderInvestmentButton(forceShow);
        RenderFeatureButton(forceShow);
        RenderTeamButton(forceShow);
        RenderMarketingButton(forceShow);
        RenderServerButton(false);

        RenderMonetizationButton(forceShow);
        RenderOffersButton(false);
    }

    private void RenderOffersButton(bool forceShow)
    {
        Draw(OffersButton, false || forceShow);
    }

    private void RenderMissionsButton(bool forceShow)
    {
        var goalsCount = MyCompany.companyGoal.Goals.Count;
        var newGoalsCount = Investments.GetNewGoals(MyCompany, Q).Count;

        bool completedGoals = MyCompany.completedGoals.Goals.Count > 0;
        bool canCompleteGoals = Investments.IsCanCompleteAnyGoal(MyCompany, Q);

        bool hasGoals = goalsCount > 0;
        bool hasNewGoals = newGoalsCount > 0;

        Draw(MissionButton, true || forceShow); // hasGoals || completedGoals
        MissionButton.GetComponent<Blinker>().enabled = canCompleteGoals;


        //Draw(GoalCounter, hasGoals);
        GoalCounter.GetComponent<Image>().enabled = hasNewGoals;
        GoalCounter.GetComponentInChildren<Image>().enabled = hasNewGoals;

        GoalCounter.GetComponentInChildren<Text>().text = hasGoals ? goalsCount.ToString() : newGoalsCount.ToString();
    }

    void RenderInvestmentButton(bool forceShow)
    {
        bool willBeBankruptSoon = Economy.IsWillBecomeBankruptOnNextPeriod(Q, Flagship);
        bool skippedSomeTime = CurrentIntDate > 60;
        //bool hadBankruptcyWarning =  || ; // NotificationUtils.GetPopupContainer(Q).seenPopups.PopupTypes.Contains(PopupType.BankruptcyThreat);

        Draw(InvestmentButton, willBeBankruptSoon || skippedSomeTime || forceShow);
    }

    void RenderFeatureButton(bool forceShow)
    {
        //var features = Products.GetProductFeaturesList(Flagship, Q).Length;
        var features = Products.GetUpgradeableRetentionFeatures(Flagship).Count();
        FeatureCounter.GetComponentInChildren<Text>().text = features.ToString();


        Draw(FeatureCounter, false && features > 0 || forceShow);
        Draw(DevelopmentButton, HasOrCompletedGoal(Flagship, InvestorGoalType.ProductPrototype) || forceShow);
    }

    void RenderMonetizationButton(bool forceShow)
    {
        var features = Products.GetUpgradeableMonetizationFeatures(Flagship).Count();
        MonetizationFeatureCounter.GetComponentInChildren<Text>().text = features.ToString();

        Draw(MonetizationFeatureCounter, features > 0 || forceShow);
        Draw(MonetizationButton, false || forceShow); // HasOrCompletedGoal(Flagship, InvestorGoalType.ProductStartMonetising)
    }

    void RenderMarketingButton(bool forceShow)
    {
        var channels = Markets.GetAffordableMarketingChannels(Flagship, Q).Count();
        var isLosingAudience = Marketing.GetChurnClients(Flagship) > 0;

        if (isLosingAudience)
            channels += 1;

        MarketingCounter.GetComponentInChildren<Text>().text = channels.ToString();


        Draw(MarketingCounter, channels > 0 || forceShow);
        Draw(MarketingButton, HasOrCompletedGoal(Flagship, InvestorGoalType.ProductFirstUsers) || forceShow);
    }


    void RenderServerButton(bool forceShow)
    {
        bool serverOverload = Products.IsNeedsMoreServers(Flagship);

        //Draw(ServersButton, serverOverload || HasOrCompletedGoal(Flagship, InvestorGoalType.ProductPrepareForRelease));
        Draw(ServersButton, false || forceShow);
    }

    void RenderTeamButton(bool forceShow)
    {
        int teamInterrupts = Flagship.team.Teams.Count(t => Teams.IsTeamNeedsAttention(Flagship, t, Q));
        TeamCounter.GetComponentInChildren<Text>().text = teamInterrupts.ToString();

        Draw(TeamCounter, teamInterrupts > 0);
        Draw(TeamsButton, HasOrCompletedGoal(Flagship, InvestorGoalType.ProductPrepareForRelease) || forceShow);
    }

    bool HasOrCompletedGoal(GameEntity company, InvestorGoalType goalType)
    {
        return company.companyGoal.Goals.Any(g => g.InvestorGoalType == goalType) || Investments.IsGoalCompleted(company, goalType);
    }

    // -------------- TABS ------------------

    void CloseTabs()
    {
        OpenUrl("/Holding/Main");
    }

    public void OnDevelopmentTabLeave()
    {
        CloseTabs();
    }

    public void OnMarketingTabLeave()
    {
        CloseTabs();
    }

    public void OnTeamTabLeave()
    {
        CloseTabs();
    }
}

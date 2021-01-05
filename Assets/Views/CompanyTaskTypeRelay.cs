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

        RenderMissionsButton();
        RenderInvestmentButton();
        RenderFeatureButton();
        RenderTeamButton();
        RenderMarketingButton();
        RenderServerButton();

        RenderMonetizationButton();
        RenderOffersButton();
    }

    private void RenderOffersButton()
    {
        Draw(OffersButton, false);
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

    void RenderInvestmentButton()
    {
        bool willBeBankruptSoon = Economy.IsWillBecomeBankruptOnNextPeriod(Q, Flagship);
        bool skippedSomeTime = CurrentIntDate > 60;
        //bool hadBankruptcyWarning =  || ; // NotificationUtils.GetPopupContainer(Q).seenPopups.PopupTypes.Contains(PopupType.BankruptcyThreat);

        Draw(InvestmentButton, willBeBankruptSoon || skippedSomeTime);
    }

    void RenderFeatureButton()
    {
        //var features = Products.GetProductFeaturesList(Flagship, Q).Length;
        var features = Products.GetUpgradeableRetentionFeatures(Flagship).Count();
        FeatureCounter.GetComponentInChildren<Text>().text = features.ToString();


        Draw(FeatureCounter, false && features > 0);
        Draw(DevelopmentButton, HasOrCompletedGoal(Flagship, InvestorGoalType.ProductPrototype));
    }

    void RenderMonetizationButton()
    {
        var features = Products.GetUpgradeableMonetizationFeatures(Flagship).Count();
        MonetizationFeatureCounter.GetComponentInChildren<Text>().text = features.ToString();

        Draw(MonetizationFeatureCounter, features > 0);
        Draw(MonetizationButton, false); // HasOrCompletedGoal(Flagship, InvestorGoalType.ProductStartMonetising)
    }

    void RenderMarketingButton()
    {
        var channels = Markets.GetAffordableMarketingChannels(Flagship, Q).Count();
        var isLosingAudience = Marketing.GetChurnClients(Flagship) > 0;

        if (isLosingAudience)
            channels += 1;

        MarketingCounter.GetComponentInChildren<Text>().text = channels.ToString();


        Draw(MarketingCounter, channels > 0);
        Draw(MarketingButton, HasOrCompletedGoal(Flagship, InvestorGoalType.ProductFirstUsers));
    }


    void RenderServerButton()
    {
        bool serverOverload = Products.IsNeedsMoreServers(Flagship);

        //Draw(ServersButton, serverOverload || HasOrCompletedGoal(Flagship, InvestorGoalType.ProductPrepareForRelease));
        Draw(ServersButton, false);
    }

    void RenderTeamButton()
    {
        int teamInterrupts = Flagship.team.Teams.Count(t => Teams.IsTeamNeedsAttention(Flagship, t, Q));
        TeamCounter.GetComponentInChildren<Text>().text = teamInterrupts.ToString();

        Draw(TeamCounter, teamInterrupts > 0);
        Draw(TeamsButton, HasOrCompletedGoal(Flagship, InvestorGoalType.ProductPrepareForRelease));
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

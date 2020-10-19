using Assets.Core;
using UnityEngine;
using UnityEngine.UI;

public class InestmentProposalScreen : View
{
    public GameObject StartRoundButton;

    public GameObject CurrentInvestorsPanel;
    public GameObject MyCompanyControl;

    public GameObject GrowthStrategyTab;
    public GameObject VotingStrategyTab;
    public GameObject ExitStrategyTab;

    public GameObject InvestorPanel;
    public GameObject Offer;

    [Header("Offer steps")]
    public GameObject GoalPanel;
    public GameObject SumPanel;
    public GameObject UrgencyPanel;
    public GameObject PossibleInvestorsPanel;
    public GameObject PossibleInvestorsLabel;

    public GameObject CompanyCost;


    public Text CompanyShare;
    public Text TotalOffer;

    public GameObject[] OfferPanels => new GameObject[] { GoalPanel, SumPanel, UrgencyPanel, PossibleInvestorsPanel };
    public GameObject[] StrategyPanels => new GameObject[] { GrowthStrategyTab, VotingStrategyTab, ExitStrategyTab };
    public GameObject[] PossibleInvestorsTabs => new GameObject[] { PossibleInvestorsPanel, PossibleInvestorsLabel, TotalOffer.gameObject };

    long Sum = -1;
    InvestorGoal InvestorGoal;
    bool goalWasChosen = false;
    int urgency = -1;

    bool isRoundActive => Companies.IsInvestmentRoundStarted(MyCompany); // MyCompany.hasAcceptsInvestments;

    bool noGrowth => MyCompany.investmentStrategy.GrowthStyle == CompanyGrowthStyle.None;
    bool noExit => MyCompany.investmentStrategy.InvestorInterest == InvestorInterest.None;
    bool noVoting => MyCompany.investmentStrategy.VotingStyle == VotingStyle.None;
    bool needsToSetStrategies => noGrowth || noExit || noVoting;

    bool settingsAreOk => !needsToSetStrategies;

    private void OnEnable()
    {
        ResetOffer();
    }

    void ResetOffer()
    {
        Sum = -1;
        goalWasChosen = false;
        urgency = -1;

        ViewRender();
    }

    public override void ViewRender()
    {
        base.ViewRender();

        Debug.Log("InvestmentProposalScreen");


        //StartRoundButton.GetComponentInChildren<Button>().interactable = !isRoundActive;
        //StartRoundButton.GetComponent<Blinker>().enabled = !isRoundActive;


        Draw(GrowthStrategyTab, isRoundActive && noGrowth);
        Draw(VotingStrategyTab, isRoundActive && !noGrowth && noVoting);
        Draw(ExitStrategyTab,   isRoundActive && !noGrowth && !noVoting && noExit);

        HideAll(PossibleInvestorsTabs);
        Hide(StartRoundButton);


        Draw(InvestorPanel, !isRoundActive);
        Draw(CompanyCost, isRoundActive);

        //Draw(InvestorPanel, isRoundActive && settingsAreOk);
        Draw(Offer,         isRoundActive && settingsAreOk);

        if (isRoundActive && settingsAreOk)
        {
            HideAll(OfferPanels);

            // RenderOffer
            if (urgency < 0)
            {
                ChooseUrgency();
            }
            else if (!goalWasChosen)
            {
                ChooseGoal();
            }
            else if (Sum < 0)
            {
                ChooseSum();
            }
            else
            {
                ChoosePossibleInvestments();
            }
        }
    }

    public void ChooseGoal()
    {
        ShowOnly(GoalPanel, OfferPanels);

        FindObjectOfType<RenderCompanyGoalListView>().ViewRender();
    }

    public void ChooseSum()
    {
        Show(SumPanel);

        goalWasChosen = true;
    }

    public void ChooseUrgency()
    {
        //Show(UrgencyPanel);
        ShowOnly(UrgencyPanel, OfferPanels);
    }

    public void ChoosePossibleInvestments()
    {
        Show(PossibleInvestorsLabel);
        //ShowOnly(PossibleInvestorsPanel, OfferPanels);

    }

    // ----------------------

    public void ChangeSum(System.Single slider)
    {
        var cost = Economy.CostOf(MyCompany, Q);
        var percent = (int)(slider);

        Sum = cost * percent / 100;

        var proposals = Companies.GetInvestmentProposals(MyCompany);
        var investorsCount = Mathf.Clamp(proposals.Count, 1, 100);

        long weeklyGain = 0;

        foreach (var p in proposals)
        {
            p.Investment.Offer = Sum / investorsCount;
            p.Investment.Portion = p.Investment.Offer / p.Investment.OfferDuration;

            weeklyGain += p.Investment.Portion;
        }


        // draw
        ShowAll(PossibleInvestorsTabs);
        Show(StartRoundButton);

        CompanyShare.text = $"for {percent}% of company";
        PossibleInvestorsPanel.GetComponentInChildren<ShareholderProposalsListView>().ViewRender();
        TotalOffer.text = $"You will get {Visuals.Positive("+" + Format.Money(weeklyGain))} / week ({Format.MinifyMoney(Sum)} total)";
    }

    public void SetUrgency(int days)
    {
        ResetOffer();
        urgency = days;

        ViewRender();
    }

    public void SetGoal(InvestorGoal investorGoal)
    {
        //ResetOffer();
        InvestorGoal = investorGoal;
        goalWasChosen = true;
        //Sum = -1;

        ViewRender();
    }
}

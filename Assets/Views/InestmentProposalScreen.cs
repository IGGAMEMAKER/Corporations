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

    public GameObject[] OfferPanels => new GameObject[] { GoalPanel, SumPanel, UrgencyPanel, PossibleInvestorsPanel };

    public Text CompanyShare;

    long Sum = -1;
    InvestorGoal InvestorGoal;

    bool goalWasChosen = false;

    int urgency = -1;

    public override void ViewRender()
    {
        base.ViewRender();

        Debug.Log("InvestmentProposalScreen");

        bool isRoundActive = MyCompany.hasAcceptsInvestments;

        StartRoundButton.GetComponentInChildren<Button>().interactable = !isRoundActive;
        StartRoundButton.GetComponent<Blinker>().enabled = !isRoundActive;

        bool noGrowth   = MyCompany.investmentStrategy.GrowthStyle == CompanyGrowthStyle.None;
        bool noExit     = MyCompany.investmentStrategy.InvestorInterest == InvestorInterest.None;
        bool noVoting   = MyCompany.investmentStrategy.VotingStyle == VotingStyle.None;

        bool needsToSetStrategies = noGrowth || noExit || noVoting;

        Draw(GrowthStrategyTab, isRoundActive && noGrowth);
        Draw(VotingStrategyTab, isRoundActive && !noGrowth && noVoting);
        Draw(ExitStrategyTab,   isRoundActive && !noGrowth && !noVoting && noExit);

        Draw(InvestorPanel, (isRoundActive && !needsToSetStrategies) || !isRoundActive);

        Draw(Offer, isRoundActive && !needsToSetStrategies);


        if (isRoundActive && !needsToSetStrategies)
        {
            Hide(StartRoundButton);
            Show(MyCompanyControl);
            Hide(InvestorPanel);

            RenderOffer();
        }
        else
        {
            Show(MyCompanyControl);
            Show(InvestorPanel);

        }
    }

    private void OnEnable()
    {
        ResetOffer();
    }

    void ResetOffer()
    {
        Sum = -1;
        goalWasChosen = false;
        urgency = -1;
    }

    void RenderOffer()
    {
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

    public void ChooseGoal()
    {
        Debug.Log("Choose goal");

        ShowOnly(GoalPanel, OfferPanels);

        FindObjectOfType<RenderCompanyGoalListView>().ViewRender();
    }

    public void ChooseSum()
    {
        //ShowOnly(SumPanel, OfferPanels);
        Show(SumPanel);
        goalWasChosen = true;
    }

    public void ChooseUrgency()
    {
        ShowOnly(UrgencyPanel, OfferPanels);
    }

    public void ChoosePossibleInvestments()
    {
        ShowOnly(PossibleInvestorsPanel, OfferPanels);
    }

    // ----------------------

    public void ChangeSum(System.Single slider)
    {
        var cost = Economy.CostOf(MyCompany, Q);
        var percent = (int)(slider);

        Sum = cost * percent / 100;

        CompanyShare.text = $"for {percent}% of company";

        Show(PossibleInvestorsPanel);
    }

    public void SetUrgency(int days)
    {
        urgency = days;

        Sum = -1;

        ViewRender();
    }

    public void SetGoal(InvestorGoal investorGoal)
    {
        InvestorGoal = investorGoal;
        goalWasChosen = true;
        Sum = -1;

        ViewRender();
    }
}

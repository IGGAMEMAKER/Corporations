using UnityEngine;
using UnityEngine.UI;

public class InestmentProposalScreen : View
{
    public GameObject StartRoundButton;

    public GameObject GrowthStrategyTab;
    public GameObject VotingStrategyTab;
    public GameObject ExitStrategyTab;

    public GameObject InvestorPanel;
    public GameObject Offer;

    public override void ViewRender()
    {
        base.ViewRender();

        bool isRoundActive = MyCompany.hasAcceptsInvestments;

        StartRoundButton.GetComponentInChildren<Button>().interactable = !isRoundActive;
        StartRoundButton.GetComponent<Blinker>().enabled = !isRoundActive;

        bool noGrowth = MyCompany.investmentStrategy.GrowthStyle == CompanyGrowthStyle.None;
        bool noExit = MyCompany.investmentStrategy.InvestorInterest == InvestorInterest.None;

        bool needsToSetStrategies = noGrowth || noExit;
        Draw(GrowthStrategyTab, isRoundActive && noGrowth);

        Draw(ExitStrategyTab, isRoundActive && noExit && !noGrowth);

        Draw(InvestorPanel, (isRoundActive && !needsToSetStrategies) || !isRoundActive);
    }
}

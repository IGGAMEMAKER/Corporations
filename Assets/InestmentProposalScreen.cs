using UnityEngine;
using UnityEngine.UI;

public class InestmentProposalScreen : View
{
    public Button StartRoundButton;
    public Text RoundCounter;

    public GameObject IPOLabel;
    public GameObject IPOButton;

    public override void ViewRender()
    {
        base.ViewRender();

        Render();
    }

    void Render()
    {
        bool isRoundActive = SelectedCompany.hasAcceptsInvestments;

        StartRoundButton.interactable = !isRoundActive;

        if (isRoundActive)
        {
            int days = SelectedCompany.acceptsInvestments.DaysLeft;
            RoundCounter.text = "This round will be active for " + days + " days";
        }
        else
        {
            RoundCounter.text = "";
        }

        RenderActionButtons();
    }

    void RenderMyInvestmentActions(bool show)
    {
        StartRoundButton.gameObject.SetActive(show);
        IPOButton.SetActive(show);
        IPOLabel.SetActive(show);
    }

    void RenderActionButtons()
    {
        RenderMyInvestmentActions(SelectedCompany.isControlledByPlayer);
    }
}

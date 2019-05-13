using UnityEngine;
using UnityEngine.UI;

public class InestmentProposalScreen : View
    , IAnyDateListener
{
    public Button StartRoundButton;
    public Text RoundCounter;

    public GameObject IPOLabel;
    public GameObject IPOButton;

    void OnEnable()
    {
        ListenDateChanges(this);

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
        bool controllable = IsUnderPlayerControl(SelectedCompany.company.Id);

        RenderMyInvestmentActions(controllable);
    }

    void IAnyDateListener.OnAnyDate(GameEntity entity, int date)
    {
        Render();
    }
}

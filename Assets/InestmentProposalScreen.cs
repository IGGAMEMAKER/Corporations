using Assets.Utils;
using UnityEngine;
using UnityEngine.UI;

public class InestmentProposalScreen : View
{
    public Button StartRoundButton;
    public Text RoundCounter;

    public GameObject IPOLabel;
    public GameObject IPOButton;
    //public GameObject ProposalsLabel;

    void Update()
    {
        Render();
    }

    void Render()
    {
        bool isActive = SelectedCompany.hasAcceptsInvestments;

        StartRoundButton.interactable = !isActive;

        if (isActive)
        {
            int days = SelectedCompany.acceptsInvestments.DaysLeft;
            RoundCounter.text = "This round will be active for " + days + " days";
        }
        else
            RoundCounter.text = "";

        RenderActionButtons();
    }

    void RenderMyInvestmentActions(bool show)
    {
        StartRoundButton.gameObject.SetActive(show);
        IPOButton.SetActive(show);
        IPOLabel.SetActive(show);
        //ProposalsLabel.SetActive(show);
    }

    void RenderActionButtons()
    {
        bool controllable = IsUnderPlayerControl(SelectedCompany.company.Id);

        RenderMyInvestmentActions(controllable);
    }
}

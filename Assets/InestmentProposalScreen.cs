using UnityEngine.UI;

public class InestmentProposalScreen : View
{
    public Button StartRoundButton;
    public Text RoundCounter;

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
    }
}

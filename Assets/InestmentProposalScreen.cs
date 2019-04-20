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
        var round = SelectedCompany.investmentRounds;
        int days = (int) round.ActiveFor;

        StartRoundButton.interactable = !round.IsActive;
        RoundCounter.text = round.IsActive ? "This round will be active for " + days + " days" : "";
    }
}

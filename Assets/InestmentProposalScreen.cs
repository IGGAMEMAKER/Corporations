using UnityEngine;
using UnityEngine.UI;

public class InestmentProposalScreen : View
{
    public Button StartRoundButton;

    public override void ViewRender()
    {
        base.ViewRender();

        Render();
    }

    void Render()
    {
        bool isRoundActive = SelectedCompany.hasAcceptsInvestments;

        StartRoundButton.interactable = !isRoundActive;
        StartRoundButton.GetComponent<Blinker>().enabled = !isRoundActive;

        // render action buttons
        StartRoundButton.gameObject.SetActive(SelectedCompany.isControlledByPlayer);
    }
}

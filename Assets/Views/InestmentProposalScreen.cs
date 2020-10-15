using UnityEngine;
using UnityEngine.UI;

public class InestmentProposalScreen : View
{
    public GameObject StartRoundButton;

    public override void ViewRender()
    {
        base.ViewRender();

        bool isRoundActive = MyCompany.hasAcceptsInvestments;

        StartRoundButton.GetComponentInChildren<Button>().interactable = !isRoundActive;
        StartRoundButton.GetComponent<Blinker>().enabled = !isRoundActive;
    }
}

using Assets.Utils;
using UnityEngine.UI;

public class FormStrategicPartnership : ToggleButtonController
{
    public override void Execute()
    {
        if (isPartnersAlready)
            Companies.CancelStrategicPartnership(MyCompany, SelectedCompany);
        else
            Companies.SendStrategicPartnershipRequest(MyCompany, SelectedCompany, GameContext, true);
    }

    bool isPartnersAlready => Companies.IsHaveStrategicPartnershipAlready(MyCompany, SelectedCompany);

    public override void ViewRender()
    {
        base.ViewRender();

        bool isCanBePartnersTheoretically = Companies.IsCanBePartnersTheoretically(MyCompany, SelectedCompany);

        var arePartners = isPartnersAlready;

        GetComponent<Button>().interactable = isCanBePartnersTheoretically;
        GetComponentInChildren<Text>().text = arePartners ? "Break strategic partnership!" : "Sign strategic partnership";
        ToggleIsChosenComponent(arePartners);
    }
}

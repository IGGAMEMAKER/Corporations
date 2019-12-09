using Assets.Utils;
using UnityEngine.UI;

public class FormStrategicPartnership : ToggleButtonController
{
    public override void Execute()
    {
        Companies.SendStrategicPartnershipRequest(MyCompany, SelectedCompany, GameContext, true);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        bool isPartnersAlready = Companies.IsHaveStrategicPartnershipAlready(MyCompany, SelectedCompany);
        bool isCanBePartnersTheoretically = Companies.IsCanBePartnersTheoretically(MyCompany, SelectedCompany);

        GetComponent<Button>().interactable = !isPartnersAlready && isCanBePartnersTheoretically;
    }
}

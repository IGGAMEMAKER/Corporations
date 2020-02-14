using Assets.Core;
using UnityEngine.UI;

public class FormStrategicPartnership : ToggleButtonController
{
    public override void Execute()
    {
        if (isPartnersAlready)
            Companies.BreakStrategicPartnership(MyCompany, SelectedCompany);
        else
            Companies.SendStrategicPartnershipRequest(MyCompany, SelectedCompany, Q, true);
    }

    bool isPartnersAlready => Companies.IsHaveStrategicPartnershipAlready(MyCompany, SelectedCompany);

    public override void ViewRender()
    {
        base.ViewRender();

        bool isCanBePartnersTheoretically = Companies.IsCanBePartnersTheoretically(MyCompany, SelectedCompany);

        var arePartners = isPartnersAlready;

        var opinion = Companies.GetPartnershipOpinionAboutUs(MyCompany, SelectedCompany, Q);

        GetComponentInChildren<Button>().interactable = isCanBePartnersTheoretically && opinion >= 0;
        GetComponentInChildren<Text>().text = arePartners ? "Break strategic partnership!" : "Sign strategic partnership";

        var h = GetComponent<Hint>();

        h.SetHint("");
        if (opinion < 0 && !arePartners)
            h.SetHint("Will not accept partnership, because their opinion about us is negative " + opinion);


        ToggleIsChosenComponent(arePartners);
    }
}

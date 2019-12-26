using Assets.Core;
using UnityEngine.UI;

public class FormStrategicPartnership : ToggleButtonController
{
    public override void Execute()
    {
        if (isPartnersAlready)
            Companies.BreakStrategicPartnership(MyCompany, SelectedCompany);
        else
            Companies.SendStrategicPartnershipRequest(MyCompany, SelectedCompany, GameContext, true);
    }

    bool isPartnersAlready => Companies.IsHaveStrategicPartnershipAlready(MyCompany, SelectedCompany);

    public override void ViewRender()
    {
        base.ViewRender();

        bool isCanBePartnersTheoretically = Companies.IsCanBePartnersTheoretically(MyCompany, SelectedCompany);

        var arePartners = isPartnersAlready;

        var opinion = Companies.GetPartnershipOpinionAboutUs(MyCompany, SelectedCompany, GameContext);

        GetComponent<Button>().interactable = isCanBePartnersTheoretically && opinion >= 0;
        GetComponentInChildren<Text>().text = arePartners ? "Break strategic partnership!" : "Sign strategic partnership";

        GetComponent<Hint>().SetHint("");
        if (opinion < 0 && !arePartners)
            GetComponent<Hint>().SetHint("Will not accept partnership, because their opinion about us is negative " + opinion);


        ToggleIsChosenComponent(arePartners);
    }
}

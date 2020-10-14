using Assets.Core;

public class PromoteToCorporation : ButtonController
{
    public override void Execute()
    {
        var cost = Economy.CostOf(MyCompany, Q);
        var goal = C.CORPORATION_REQUIREMENTS_COMPANY_COST;

        if (cost < goal)
        {
            NotificationUtils.AddPopup(Q, new PopupMessageCorporationRequirements(MyCompany.company.Id));
        }
        else
        {
            Companies.PromoteToCorporation(MyCompany, Q);

            Navigate(ScreenMode.CorporationScreen);
        }
    }
}

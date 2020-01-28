using Assets.Core;

public class PromoteToCorporation : ButtonController
{
    public override void Execute()
    {
        var cost = Economy.GetCompanyCost(GameContext, MyCompany);
        var goal = Balance.CORPORATION_REQUIREMENTS_COMPANY_COST;

        if (cost < goal)
        {
            NotificationUtils.AddPopup(GameContext, new PopupMessageCorporationRequirements(MyCompany.company.Id));
        }
        else
        {
            Companies.PromoteToCorporation(MyCompany, GameContext);

            Navigate(ScreenMode.CorporationScreen);
        }
    }
}

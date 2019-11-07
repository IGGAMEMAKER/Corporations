using Assets.Utils;

public class PromoteToCorporation : ButtonController
{
    public override void Execute()
    {
        CompanyUtils.PromoteToCorporation(MyCompany, GameContext);

        Navigate(ScreenMode.CorporationScreen);
    }
}

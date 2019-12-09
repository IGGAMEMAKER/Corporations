using Assets.Utils;

public class PromoteToCorporation : ButtonController
{
    public override void Execute()
    {
        Companies.PromoteToCorporation(MyCompany, GameContext);

        Navigate(ScreenMode.CorporationScreen);
    }
}

public class LinkToInvestmentTab : ButtonController
{
    public int CompanyId;

    public override void Execute()
    {
        if (CurrentScreen == ScreenMode.DevelopmentScreen)
            CompanyId = MyProductEntity.company.Id;

        Navigate(ScreenMode.InvesmentsScreen, CompanyId);
    }
}

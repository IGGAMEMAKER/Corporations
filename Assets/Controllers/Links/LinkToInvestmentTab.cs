public class LinkToInvestmentTab : ButtonController
{
    public int CompanyId;

    public override void Execute()
    {
        if (CurrentScreen == ScreenMode.DevelopmentScreen)
            CompanyId = MyProductEntity.company.Id;

        NavigateToCompany(ScreenMode.InvesmentsScreen, CompanyId);
    }
}

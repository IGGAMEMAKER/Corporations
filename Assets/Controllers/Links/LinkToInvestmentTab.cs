public class LinkToInvestmentTab : ButtonController
{
    public int CompanyId;

    public override void Execute()
    {
        NavigateToCompany(ScreenMode.InvesmentsScreen, CompanyId);
    }
}

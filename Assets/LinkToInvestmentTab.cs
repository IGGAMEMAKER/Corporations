public class LinkToInvestmentTab : ButtonController
{
    public override void Execute()
    {
        Navigate(ScreenMode.InvesmentsScreen, MyProductEntity.company.Id);
    }
}

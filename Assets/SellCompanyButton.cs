public class SellCompanyButton : ButtonController
{
    public override void Execute()
    {
        SelectedCompany.isOnSales = !SelectedCompany.isOnSales;
    }
}

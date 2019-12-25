public class ToggleSellCompanyButton : View
{
    public override void ViewRender()
    {
        base.ViewRender();

        GetComponent<IsChosenComponent>().Toggle(SelectedCompany.isOnSales);
    }
}

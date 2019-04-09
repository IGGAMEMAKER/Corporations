public class FillSelectedCompanyOwnings : FillCompanyOwnings
{
    public override GameEntity GetObservableCompany()
    {
        return SelectedCompany;
    }
}

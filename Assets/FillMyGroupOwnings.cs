public class FillMyGroupOwnings : FillCompanyOwnings
{
    public override GameEntity GetObservableCompany()
    {
        return MyGroupEntity;
    }
}

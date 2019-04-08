public class LeaveCEOProductCompany : AbandonCompanyController
{
    public override GameEntity GetCompany()
    {
        return MyProductEntity;
    }
}
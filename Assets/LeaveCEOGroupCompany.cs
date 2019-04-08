public class LeaveCEOGroupCompany : AbandonCompanyController
{
    public override GameEntity GetCompany()
    {
        return MyGroupEntity;
    }
}
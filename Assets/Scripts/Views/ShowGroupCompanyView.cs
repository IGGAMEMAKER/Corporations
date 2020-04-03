public class ShowGroupCompanyView : ShowControlledCompany
{
    public override GameEntity GetControlledEntity()
    {
        return MyGroupEntity;
    }
}
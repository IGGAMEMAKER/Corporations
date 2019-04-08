public class ShowProductCompanyView : ShowControlledCompany
{
    public override GameEntity GetControlledEntity()
    {
        return MyProductEntity;
    }
}
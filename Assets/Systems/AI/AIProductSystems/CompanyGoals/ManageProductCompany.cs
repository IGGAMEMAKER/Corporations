public partial class AIProductSystems : OnDateChange
{
    void ManageProductCompany(GameEntity product)
    {
        ManageProductDevelopment(product);
    }


    void ManageProductDevelopment(GameEntity product)
    {
        UpgradeSegment(product);
    }

    void ManageInvestors(GameEntity product)
    {
        // taking investments
        TakeInvestments(product);
    }
}

public partial class AIProductSystems : OnDateChange
{
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

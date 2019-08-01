using Assets.Utils;

public partial class AIProductSystems : OnDateChange
{
    void FixLoyalty(GameEntity product)
    {
        Print("Terrible loyalty", product);

        // decrease prices
        DecreasePrices(product);

        // improve segments
        UpgradeSegment(product);
    }
}

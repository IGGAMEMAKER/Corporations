using Assets.Utils;

public partial class AIProductSystems : OnDateChange
{
    void FixLoyalty(GameEntity product)
    {
        Print("Terrible loyalty", product);

        // decrease prices
        DecreasePrices(product);

        // improve segments
        ImproveSegments(product);
    }

    void ImproveSegments(GameEntity product)
    {
        var core = MarketingUtils.GetClientLoyalty(gameContext, product.company.Id, UserType.Core);
        var regular = MarketingUtils.GetClientLoyalty(gameContext, product.company.Id, UserType.Regular);

        UpgradeSegment(product);
    }
}

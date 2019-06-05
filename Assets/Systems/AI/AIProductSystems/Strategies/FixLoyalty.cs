using Assets.Utils;

public partial class AIProductSystems : OnDateChange
{
    void FixLoyalty(GameEntity product)
    {
        // decrease prices
        DecreasePrices(product);

        // improve segments
        ImproveSegments(product);

        // match market requirements
        CatchMarketRequirements(product);
        //  // focus on ideas?
    }

    void ImproveSegments(GameEntity product)
    {
        var core = MarketingUtils.GetClientLoyalty(gameContext, product.company.Id, UserType.Core);
        var regular = MarketingUtils.GetClientLoyalty(gameContext, product.company.Id, UserType.Regular);

        if (core < regular)
            UpgradeSegment(product, UserType.Core);
        else
            UpgradeSegment(product, UserType.Regular);
    }

    void UpgradeSegment(GameEntity product, UserType userType)
    {

    }

    void CatchMarketRequirements(GameEntity product)
    {
        
    }

    void DecreasePrices(GameEntity product)
    {
        var price = product.finance.price;
        
        switch (price)
        {
            case Pricing.High: price = Pricing.Medium; break;
            case Pricing.Medium: price = Pricing.Low; break;
        }

        ProductUtils.SetPrice(product, price);
    }
}

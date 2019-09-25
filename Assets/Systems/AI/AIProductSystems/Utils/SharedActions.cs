using Assets.Utils;
using Assets.Utils.Tutorial;

// actions used in multiple strategies
public partial class AIProductSystems
{
    void UpgradeSegment(GameEntity product)
    {
        ProductUtils.UpdgradeProduct(product, gameContext);

        ProductUtils.UpgradeExpertise(product, gameContext);
    }

    void StayInMarket(GameEntity product)
    {
        if (ProductUtils.IsInMarket(product, gameContext))
            UpgradeSegment(product);
    }

    void Innovate(GameEntity product)
    {
        UpgradeSegment(product);
    } 
}

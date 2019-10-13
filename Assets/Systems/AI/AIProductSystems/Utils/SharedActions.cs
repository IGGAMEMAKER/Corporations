using Assets.Utils;

public partial class AIProductSystems
{
    void UpgradeSegment(GameEntity product)
    {
        ProductUtils.UpdgradeProduct(product, gameContext);
        ProductUtils.UpgradeExpertise(product, gameContext);

        ProductUtils.UpgradeProductImprovement(ProductImprovement.Acquisition, product);
        ProductUtils.UpgradeProductImprovement(ProductImprovement.Monetisation, product);
    }
}

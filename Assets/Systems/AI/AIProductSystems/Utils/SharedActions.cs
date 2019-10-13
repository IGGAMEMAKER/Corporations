using Assets.Utils;

public partial class AIProductSystems
{
    void UpgradeSegment(GameEntity product)
    {
        ProductUtils.UpdgradeProduct(product, gameContext);
        ProductUtils.UpgradeExpertise(product, gameContext);
    }
}

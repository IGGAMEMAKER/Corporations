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

    void Innovate(GameEntity product)
    {
        UpgradeSegment(product);
    } 
}

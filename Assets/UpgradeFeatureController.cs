using Assets.Core;

public class UpgradeFeatureController : ButtonController
{
    NewProductFeature feature;
    public override void Execute()
    {
        Products.TryToUpgradeFeature(Flagship, feature, Q);
    }

    internal void SetEntity(NewProductFeature feature)
    {
        this.feature = feature;
    }
}

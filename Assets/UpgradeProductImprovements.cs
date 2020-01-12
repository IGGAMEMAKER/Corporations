using Assets.Core;

public class UpgradeProductImprovements : ButtonController
{
    public ProductFeature ProductImprovement;

    public override void Execute()
    {
        Products.UpgradeFeatures(ProductImprovement, SelectedCompany, GameContext);
    }
}

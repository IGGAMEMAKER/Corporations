using Assets.Core;

public class UpgradeProductImprovements : ButtonController
{
    public ProductImprovement ProductImprovement;

    public override void Execute()
    {
        Products.UpgradeFeatures(ProductImprovement, SelectedCompany, GameContext);
    }
}

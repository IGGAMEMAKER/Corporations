using Assets.Core;

public class UpgradeProductImprovements : ButtonController
{
    public ProductImprovement ProductImprovement;

    public override void Execute()
    {
        Products.UpgradeProductImprovement(ProductImprovement, SelectedCompany);
    }
}

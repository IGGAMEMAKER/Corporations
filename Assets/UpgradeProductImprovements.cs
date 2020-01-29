using Assets.Core;

public class UpgradeProductImprovements : TimedButton
{
    public ProductFeature ProductImprovement;

    public override void Execute()
    {
        var product = Companies.Get(GameContext, CompanyId);

        Products.UpgradeFeatures(ProductImprovement, product, GameContext);
    }

    public override CompanyTask GetCompanyTask()
    {
        var product = Companies.Get(GameContext, CompanyId);

        return new CompanyTaskUpgradeFeature(product.company.Id, ProductImprovement);
    }

    public override bool IsInteractable()
    {
        var company = Companies.Get(GameContext, CompanyId);

        return Products.HasFreeImprovements(company) && Cooldowns.CanAddTask(GameContext, GetCompanyTask());
    }

    public override string ShortTitle()
    {
        switch (ProductImprovement)
        {
            case ProductFeature.Retention: return "Upgrading App";
            default: return "Upgrading " + ProductImprovement.ToString();
        }
    }

    public override string StandardTitle()
    {
        switch (ProductImprovement)
        {
            case ProductFeature.Retention: return "Upgrade App";
            default: return "Upgrade " + ProductImprovement.ToString();
        }
    }
}

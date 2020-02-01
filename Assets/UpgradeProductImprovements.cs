using Assets.Core;

public class UpgradeProductImprovements : TimedButton
{
    public ProductFeature ProductImprovement;

    public override void Execute()
    {
        var product = Companies.Get(Q, CompanyId);

        Products.UpgradeFeatures(ProductImprovement, product, Q);
    }

    public override CompanyTask GetCompanyTask()
    {
        return new CompanyTaskUpgradeFeature(CompanyId, ProductImprovement);
    }

    public override bool IsInteractable()
    {
        var company = Companies.Get(Q, CompanyId);

        return Products.HasFreeImprovements(company) && Cooldowns.CanAddTask(Q, GetCompanyTask());
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

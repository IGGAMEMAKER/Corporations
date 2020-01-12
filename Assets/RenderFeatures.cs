using Assets.Core;

public class RenderFeatures : ParameterView
{
    public override string RenderValue()
    {
        var product = SelectedCompany;

        var freeFeatures = Products.GetFreeImprovements(product);

        if (Cooldowns.IsHasTask(GameContext, new CompanyTaskUpgradeFeature(product.company.Id, ProductFeature.Acquisition)))
            freeFeatures -= 1;

        return freeFeatures.ToString();
    }
}

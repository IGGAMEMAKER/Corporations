using Assets.Core;

public class RenderFeatures : ParameterView
{
    public override string RenderValue()
    {
        var product = SelectedCompany;

        var freeFeatures = Products.GetFreeImprovements(product);

        var freeFeaturesDescription = "";
        if (freeFeatures > 0)
            freeFeaturesDescription = $"(+{freeFeatures})";

        var features = product.productImprovements.Count;

        return $"{features} {freeFeaturesDescription}";
    }
}

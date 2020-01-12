public class RenderChurnFeatures : ParameterView
{
    public override string RenderValue()
    {
        var product = SelectedCompany;

        var retention = product.features.features[ProductFeature.Retention];

        return retention.ToString();
    }
}

public class RenderChurnFeatures : ParameterView
{
    public override string RenderValue()
    {
        var product = SelectedCompany;

        var retention = product.features.features[ProductImprovement.Retention];

        return retention.ToString();
    }
}

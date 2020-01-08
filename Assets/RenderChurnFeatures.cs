public class RenderChurnFeatures : ParameterView
{
    public override string RenderValue()
    {
        var product = SelectedCompany;

        var retention = product.productImprovements.Improvements[ProductImprovement.Retention];

        return retention.ToString();
    }
}

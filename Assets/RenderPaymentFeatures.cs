public class RenderPaymentFeatures : ParameterView
{
    public override string RenderValue()
    {
        var product = SelectedCompany;

        var retention = product.productImprovements.Improvements[ProductImprovement.Monetisation];

        return retention.ToString();
    }
}
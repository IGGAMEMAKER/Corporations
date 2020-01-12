public class RenderPaymentFeatures : ParameterView
{
    public override string RenderValue()
    {
        return SelectedCompany.features.features[ProductImprovement.Monetisation].ToString();
    }
}
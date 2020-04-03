public class RenderPaymentFeatures : ParameterView
{
    public override string RenderValue()
    {
        return SelectedCompany.features.features[ProductFeature.Monetisation].ToString();
    }
}
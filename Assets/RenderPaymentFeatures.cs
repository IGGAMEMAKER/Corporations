public class RenderPaymentFeatures : ParameterView
{
    public override string RenderValue()
    {
        return SelectedCompany.productImprovements.Improvements[ProductImprovement.Monetisation].ToString();
    }
}
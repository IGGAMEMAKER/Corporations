using Assets.Core;

public class PaymentsFeaturesListView : ProductFeaturesListView
{
    public override NewProductFeature[] GetFeatures()
    {
        return Products.GetMonetisationFeatures(company);
    }
}

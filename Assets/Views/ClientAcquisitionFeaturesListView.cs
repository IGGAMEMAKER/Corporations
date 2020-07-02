using Assets.Core;

public class ClientAcquisitionFeaturesListView : ProductFeaturesListView
{
    public override NewProductFeature[] GetFeatures()
    {
        return Products.GetAcquisitionFeatures(company);
    }
}

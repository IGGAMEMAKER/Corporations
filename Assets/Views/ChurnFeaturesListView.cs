using Assets.Core;

public class ChurnFeaturesListView : ProductFeaturesListView
{
    public override NewProductFeature[] GetFeatures()
    {
        return Products.GetChurnFeatures(company);
    }
}

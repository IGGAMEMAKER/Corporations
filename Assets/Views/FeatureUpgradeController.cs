using Assets.Core;

public class FeatureUpgradeController : ButtonController
{
    public FeatureView FeatureView;
    public override void Execute()
    {
        var product = Flagship;

        var featureName = FeatureView.NewProductFeature.Name;

        if (!Products.IsUpgradingFeature(product, featureName))
        {
            var relay = FindObjectOfType<FlagshipRelayInCompanyView>();
            
            var task = new TeamTaskFeatureUpgrade(FeatureView.NewProductFeature);

            relay.AddPendingTask(task);

            var featureList = FindObjectOfType<RenderAllAudienceNeededFeatureListView>();

            if (featureList != null)
            {
                // view render to recalculate features count
                featureList.ViewRender();

                if (featureList.count == 0)
                {
                    CloseModal("Features");
                    // CloseMyModalWindowIfListIsBlank
                }
            }
        }

        FeatureView.ViewRender();
    }
}

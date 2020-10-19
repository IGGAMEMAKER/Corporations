using Assets.Core;
using UnityEngine;

public class RenderAllAudienceNeededFeatureListView : ListView
{
    bool RetentionFeatures = true;

    public GameObject MonetisationButton;
    public GameObject RetentionButton;

    public override void SetItem<T>(Transform t, T entity)
    {
        t.GetComponent<FeatureView>().SetFeature(entity as NewProductFeature);
    }

    public void ShowRetentionFeatures()
    {
        RetentionFeatures = true;

        ViewRender();
    }

    public void ShowMonetisationFeatures()
    {
        RetentionFeatures = false;

        ViewRender();
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var company = Flagship;

        var retentionFeatures = Products.GetUpgradeableRetentionFeatures(company, Q);
        var monetisationFeatures = Products.GetUpgradeableMonetisationFeatures(company, Q);

        var features = RetentionFeatures ? retentionFeatures : monetisationFeatures;

        if (MonetisationButton != null)
            Draw(MonetisationButton, company.isRelease && monetisationFeatures.Length > 0);

        if (RetentionButton != null)
        {
            Draw(RetentionButton, retentionFeatures.Length > 0);
        }

        SetItems(features);
    }

    private void OnEnable()
    {
        ShowRetentionFeatures();
    }
}

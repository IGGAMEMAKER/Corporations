using Assets.Core;
using System.Collections.Generic;
using UnityEngine;

public class RenderAllAudienceNeededFeatureListView : ListView
{
    public bool RetentionFeatures = true;
    public bool MonetizationFeatures = true;

    public AudiencesOnMainScreenListView AudiencesOnMainScreenListView;
    
    private void OnEnable()
    {
        ViewRender();
    }

    public override void SetItem<T>(Transform t, T entity)
    {
        t.GetComponent<FeatureView>().SetFeature(entity as NewProductFeature, AudiencesOnMainScreenListView);
    }

    //public void ShowRetentionFeatures()
    //{
    //    RetentionFeatures = true;

    //    ViewRender();
    //}

    //public void ShowMonetisationFeatures()
    //{
    //    RetentionFeatures = false;

    //    ViewRender();
    //}

    public override void ViewRender()
    {
        base.ViewRender();

        var company = Flagship;

        var retentionFeatures = Products.GetUpgradeableRetentionFeatures(company, Q);
        var monetisationFeatures = Products.GetUpgradeableMonetisationFeatures(company, Q);

        List<NewProductFeature> features = new List<NewProductFeature>();

        if (RetentionFeatures)
        {
            features.AddRange(retentionFeatures);
        }

        if (MonetizationFeatures)
        {
            features.AddRange(monetisationFeatures);
        }
        //RetentionFeatures ? retentionFeatures : monetisationFeatures;

        SetItems(features);
    }
}

using Assets.Core;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RenderAllAudienceNeededFeatureListView : ListView
{
    public bool RetentionFeatures = true;
    public bool MonetizationFeatures = true;

    public GameObject PendingTaskIcon;
    public Text AmountOfFeatures;

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

        if (features.Count > 0)
        {
            var p = new TeamTaskFeatureUpgrade(features[0]);
            var activeFeatures = Teams.GetActiveSlots(company, p);

            var pending = Teams.GetPendingFeaturesAmount(company, p);


            AmountOfFeatures.text = $"{activeFeatures}";
            if (pending > 0)
                AmountOfFeatures.text += $"+{Visuals.Colorize(pending.ToString(), "orange")}";

            Draw(PendingTaskIcon, pending > 0);
        }
        else
        {
            AmountOfFeatures.text = "";
            Hide(PendingTaskIcon);
        }

        SetItems(features);
    }
}

using Assets.Core;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RenderAllAudienceNeededFeatureListView : ListView
{
    public bool RetentionFeatures = true;
    public bool MonetizationFeatures = true;

    public GameObject PendingTaskIcon;
    public Text AmountOfFeatures;

    public Text AmountOfSlots;

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

        var retentionFeatures = Products.GetUpgradeableRetentionFeatures(company);
        var monetisationFeatures = Products.GetUpgradeableMonetizationFeatures(company);

        List<NewProductFeature> features = new List<NewProductFeature>();

        if (RetentionFeatures)
        {
            features.AddRange(retentionFeatures);
        }

        if (MonetizationFeatures)
        {
            features.AddRange(monetisationFeatures);
        }

        // ----------------------------------------------------
        var p = new TeamTaskFeatureUpgrade(features.FirstOrDefault());
        var activeTasks = Teams.GetActiveSameTaskTypeSlots(company, p);

        var pending = Teams.GetPendingSameTypeTaskAmount(company, p);


        AmountOfFeatures.text = $"{activeTasks}";
        if (pending > 0)
            AmountOfFeatures.text += $"+{Visuals.Colorize(pending.ToString(), "orange")}";

        Draw(PendingTaskIcon, pending > 0);

        AmountOfSlots.text = Visuals.Colorize((long)Teams.GetSlotsForTaskType(company, p));
        // ----------------------------------------------------

        SetItems(features);
    }
}

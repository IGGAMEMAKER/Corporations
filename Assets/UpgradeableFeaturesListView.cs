using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeableFeaturesListView : ListView
{
    public Text NoAvailableFeaturesText;

    public override void SetItem<T>(Transform t, T entity)
    {
        t.GetComponent<FeatureView>().SetFeature(entity as NewProductFeature, null);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        bool isMonetizationMission = Flagship.companyGoal.Goals.Any(g => g.InvestorGoalType == InvestorGoalType.ProductStartMonetising);

        var features = Products.GetUpgradeableRetentionFeatures(Flagship).Take(2).Where(f => !isMonetizationMission);

        var count = features.Count();

        if (count == 0)
        {
            bool hasFreeSlot = Teams.HasFreeSlotForTeamTask(Flagship, Teams.GetDevelopmentTaskMockup());

            if (hasFreeSlot)
            {
                var maxLvl = Teams.GetMaxFeatureRatingCap(Flagship, Q).Sum();
                NoAvailableFeaturesText.text = Visuals.Negative("Increase <b>max feature level</b> to upgrade more features");
                //NoAvailableFeaturesText.text = $"You've upgraded all features to max level ({maxLvl}lvl).\n\n{Visuals.Negative("Increase max feature level it to upgrade more features")}";
            }
            else
            {
                NoAvailableFeaturesText.text =  $"All teams are busy. <b>Hire</b> or <b>promote</b> more teams, to make more features";
            }

            if (isMonetizationMission)
            {
                if (hasFreeSlot)
                {
                    NoAvailableFeaturesText.text = Visuals.Positive("Add <b>Monetization features</b> to start making money!");
                }
                else
                {
                    NoAvailableFeaturesText.text = "You will get <b>Monetization features</b> after finishing current features";
                }
            }
        }

        Draw(NoAvailableFeaturesText, count == 0);

        SetItems(features);
    }

    private void OnEnable()
    {
        ViewRender();
    }
}

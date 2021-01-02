using Assets.Core;
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

        var company = Flagship;

        bool isMonetizationMission =
            company.companyGoal.Goals.Any(g => g.InvestorGoalType == InvestorGoalType.ProductStartMonetising);

        var features = Products.GetUpgradeableRetentionFeatures(company)
            .OrderBy(f => Marketing.GetLoyaltyChangeFromFeature(company, f) / Teams.GetFeatureUpgradeCost(company, new TeamTaskFeatureUpgrade(f)))
            
            .Take(1).Where(f => !isMonetizationMission);

        var featureCount = features.Count();

        RenderNoAvailableFeatures(isMonetizationMission, featureCount);

        SetItems(features);
    }

    void RenderNoAvailableFeatures(bool isMonetizationMission, int featureCount)
    {
        if (featureCount == 0)
        {
            bool hasFreeSlot = Teams.HasFreeSlotForTeamTask(Flagship, Teams.GetDevelopmentTaskMockup());

            if (hasFreeSlot)
            {
                NoAvailableFeaturesText.text =
                    Visuals.Negative("Get more <b>expertise</b> to upgrade more features");
            }
            else
            {
                NoAvailableFeaturesText.text =
                    $"All teams are busy. <b>HIRE</b> or <b>PROMOTE</b> more TEAMS, to make more features";
            }

            if (isMonetizationMission)
            {
                if (hasFreeSlot)
                {
                    NoAvailableFeaturesText.text =
                        Visuals.Positive("Add <b>Monetization features</b> to start making money!");
                }
                else
                {
                    NoAvailableFeaturesText.text =
                        "You will get <b>Monetization features</b> after finishing current features";
                }
            }
        }

        Draw(NoAvailableFeaturesText, featureCount == 0);
    }

    private void OnEnable()
    {
        ViewRender();
    }
}

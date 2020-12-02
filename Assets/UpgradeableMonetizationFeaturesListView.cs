using Assets.Core;
using System.Linq;
using UnityEngine;

public class UpgradeableMonetizationFeaturesListView : ListView
{
    public override void SetItem<T>(Transform t, T entity)
    {
        t.GetComponent<FeatureView>().SetFeature(entity as NewProductFeature, null);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        bool canMonetize = Flagship.companyGoal.Goals.Any(g => g.InvestorGoalType == InvestorGoalType.ProductStartMonetising) || Flagship.completedGoals.Goals.Any(g => g == InvestorGoalType.ProductStartMonetising);

        var features = Products.GetUpgradeableMonetizationFeatures(Flagship).Take(canMonetize ? 1 : 0);

        SetItems(features);
    }

    private void OnEnable()
    {
        ViewRender();
    }
}

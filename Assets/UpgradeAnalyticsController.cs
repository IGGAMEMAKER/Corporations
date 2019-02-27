using Entitas;
using UnityEngine;

public class UpgradeAnalyticsController : ButtonController
{
    void OnUpgradeAnalytics()
    {
        ProductComponent product = ControlledProduct;
        
        SendEvent().AddEventUpgradeAnalytics(product.Id);
        Debug.Log("OnUpgradeAnalytics");
    }

    public override void Execute() => OnUpgradeAnalytics();
}

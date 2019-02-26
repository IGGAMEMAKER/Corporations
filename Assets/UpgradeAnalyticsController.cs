using UnityEngine;

public class UpgradeAnalyticsController : ButtonController
{
    void OnUpgradeAnalytics()
    {
        Debug.Log("OnUpgradeAnalytics");
    }

    public override void Execute() => OnUpgradeAnalytics();
}

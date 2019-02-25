using Assets;
using Assets.Classes;
using Unity.Entities;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeAnalyticsController : ButtonController
{
    void OnUpgradeAnalytics()
    {
        Debug.Log("OnUpgradeAnalytics");
    }

    public override void Execute() => OnUpgradeAnalytics();
}

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

        //if (DataManager.instance.world.projects[0].Analytics < 10)
        //{
        //    DataManager.instance.world.projects[0].Analytics++;
        //}
    }

    public override void Execute() => OnUpgradeAnalytics();
}

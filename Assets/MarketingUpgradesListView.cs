using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketingUpgradesListView : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        var upgrade = (TeamImprovement)(object)entity;

        t.GetComponent<TeamUpgradeView>().SetEntity(upgrade);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var upgrades = new TeamImprovement[]
        {

    //            BaseMarketing, // +1
    //AggressiveMarketing, // +3
    //AllPlatformMarketing, // bigger maintenance and reach when getting clients

    //ClientSupport, // -1% churn fixed cost
    //ImprovedClientSupport, // -1% churn scaling cost
        };



        SetItems(upgrades);
    }
}

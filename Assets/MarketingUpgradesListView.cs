using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketingUpgradesListView : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        var upgrade = (TeamImprovement)(object)entity;

        t.GetComponent<SetTeamUpgrade>().SetEntity(upgrade);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var upgrades = new TeamImprovement[]
        {
        new TeamImprovement
        {
            TeamUpgrade = TeamUpgrade.BaseMarketing,
            Name = "Base Marketing",
            Description = "Gives you +1 Brand power each month. Costs money",
            Workers = 1
        },

        };



        SetItems(upgrades);
    }
}

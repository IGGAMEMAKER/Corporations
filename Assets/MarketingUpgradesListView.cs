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
                Name = "Base marketing",
                Description = "Gives you +1 Brand power each month. Costs money",
                Workers = 1
            },
            new TeamImprovement
            {
                TeamUpgrade = TeamUpgrade.AggressiveMarketing,
                Name = "Aggressive marketing",
                Description = "Gives you +3 Brand power each month. Costs a lot of money",
                Workers = 5
            },
            new TeamImprovement
            {
                TeamUpgrade = TeamUpgrade.AllPlatformMarketing,
                Name = "All platform marketing",
                Description = "Gives you +3 Brand power each month. Costs a lot of money",
                Workers = 5
            },
            new TeamImprovement
            {
                TeamUpgrade = TeamUpgrade.ClientSupport,
                Name = "Client support",
                Description = "Lowers churn rate by 1%",
                Workers = 1
            },
            new TeamImprovement
            {
                TeamUpgrade = TeamUpgrade.ImprovedClientSupport,
                Name = "Improved client support",
                Description = "Lowers churn rate by 1% but costs additional money",
                Workers = 5
            },
    //            BaseMarketing, // +1
    //AggressiveMarketing, // +3
    //AllPlatformMarketing, // bigger maintenance and reach when getting clients

    //ClientSupport, // -1% churn fixed cost
    //ImprovedClientSupport, // -1% churn scaling cost
        };



        SetItems(upgrades);
    }
}

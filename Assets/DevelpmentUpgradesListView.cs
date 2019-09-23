using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public struct TeamImprovement
{
    public TeamUpgrade TeamUpgrade;
    public string Name;
    public string Description;
    public int Workers;
}

public class DevelpmentUpgradesListView : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        var upgrade = (TeamImprovement) (object) entity;

        t.GetComponent<TeamUpgradeView>().SetEntity(upgrade);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var upgrades = new TeamImprovement[]
        {
            // dev
            new TeamImprovement
            {
                TeamUpgrade = TeamUpgrade.Prototype,
                Name = "Prototype",
                Description = "Makes and app without any monetisation",
                Workers = 1
            },
            new TeamImprovement
            {
                TeamUpgrade = TeamUpgrade.OnePlatformPaid,
                Name = "Polished App",
                Description = "Makes and app with monetisation",
                Workers = 4
            },
            new TeamImprovement
            {
                TeamUpgrade = TeamUpgrade.CrossplatformDevelopment,
                Name = "All platforms",
                Description = "Increases your income by 300%",
                Workers = 30
            },

            // marketing
            new TeamImprovement
            {
                TeamUpgrade = TeamUpgrade.BaseMarketing,
                Name = "Base marketing",
                Description = "+1 Brand power each month. Costs money",
                Workers = 1
            },
            new TeamImprovement
            {
                TeamUpgrade = TeamUpgrade.AggressiveMarketing,
                Name = "Aggressive marketing",
                Description = "+3 Brand power each month. Costs a lot of money",
                Workers = 7
            },
            new TeamImprovement
            {
                TeamUpgrade = TeamUpgrade.AllPlatformMarketing,
                Name = "All platform marketing",
                Description = "+1 Brand power each month when combined with 'All platforms'. Costs money",
                Workers = 5
            },

            //// support
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
        }
        .OrderBy(i => i.Workers)
        .ToArray();

        //new TeamImprovement
        //{
        //    TeamUpgrade = TeamUpgrade.BaseMarketing,
        //    Name = "Base Marketing",
        //    Description = "Gives you +1 Brand power each month. Costs money",
        //    Workers = 1
        //},

        SetItems(upgrades);
    }
}

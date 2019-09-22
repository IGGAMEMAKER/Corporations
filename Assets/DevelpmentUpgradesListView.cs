using System.Collections;
using System.Collections.Generic;
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

        t.GetComponent<SetTeamUpgrade>().SetEntity(upgrade);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var upgrades = new TeamImprovement[]
        {
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
                TeamUpgrade = TeamUpgrade.AllPlatforms,
                Name = "All platforms",
                Description = "Increases your income by 300%",
                Workers = 30
            },

        };

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

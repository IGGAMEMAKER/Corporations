using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProductTaskController : ButtonController
{
    public override void Execute()
    {
        ScheduleUtils.ResumeGame(Q);

        var features = Products.GetUpgradeableRetentionFeatures(Flagship);

        var task = new TeamTaskFeatureUpgrade(features.First());
        Teams.AddTeamTask(Flagship, CurrentIntDate, Q, 0, task);

        //Cooldowns.AddTask(Q, new CompanyTaskUpgradeFeature(Flagship.company.Id, new NewProductFeature()))
    }
}

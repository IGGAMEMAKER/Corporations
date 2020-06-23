using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddSupportFeature : ButtonController
{
    public override void Execute()
    {
        var supportFeature = GetComponent<SupportView>().SupportFeature;
        var name = supportFeature.Name;

        var product = Flagship;

        var relay = FindObjectOfType<FlagshipRelayInCompanyView>();

        var teamId = relay.ChosenTeamId;
        var taskId = relay.ChosenSlotId;

        // 
        if (!product.supportUpgrades.Upgrades.ContainsKey(name))
        {
            product.supportUpgrades.Upgrades[name] = 0;
        }

        product.supportUpgrades.Upgrades[name]++;


        Teams.AddTeamTask(product, Q, teamId, taskId, new TeamTaskSupportFeature(supportFeature));
        relay.ChooseWorkerInteractions();
    }
}

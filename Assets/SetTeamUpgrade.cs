using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTeamUpgrade : ButtonController
{
    TeamUpgrade TeamUpgrade;

    public override void Execute()
    {
        var activated = SelectedCompany.teamImprovements.Upgrades.ContainsKey(TeamUpgrade);

        if (activated)
            SelectedCompany.teamImprovements.Upgrades.Remove(TeamUpgrade);
        else
        {
            bool hasEnoughWorkers = true;

            if (hasEnoughWorkers)
                SelectedCompany.teamImprovements.Upgrades[TeamUpgrade] = 1;
        }
    }

    public void SetTeanUpgrade(TeamUpgrade teamUpgrade)
    {
        TeamUpgrade = teamUpgrade;
    }
}

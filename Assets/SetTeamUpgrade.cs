using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTeamUpgrade : ButtonController
{
    TeamUpgrade TeamUpgrade;

    public override void Execute()
    {
        TeamUtils.ToggleTeamImprovement(SelectedCompany, TeamUpgrade);
    }

    public void SetTeanUpgrade(TeamUpgrade teamUpgrade)
    {
        TeamUpgrade = teamUpgrade;
    }
}

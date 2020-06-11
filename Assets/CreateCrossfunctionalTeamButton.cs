using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCrossfunctionalTeamButton : CompanyUpgradeButton
{
    public override void Execute()
    {
        Teams.AddTeam(Flagship, TeamType.CrossfunctionalTeam);
    }

    public override string GetBenefits()
    {
        return "+1 channel and +1 feature at time";
    }

    public override string GetButtonTitle()
    {
        return "Create crossfunctional team";
    }

    public override string GetHint()
    {
        return "";
    }

    public override bool GetState()
    {
        return true;
    }
}

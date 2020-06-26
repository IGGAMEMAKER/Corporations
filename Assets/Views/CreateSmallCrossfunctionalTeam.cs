using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSmallCrossfunctionalTeam : CompanyUpgradeButton
{
    public override void Execute()
    {
        Teams.AddTeam(Flagship, TeamType.SmallCrossfunctionalTeam);
    }

    public override string GetBenefits()
    {
        return "";
        return $"+{Teams.GetAmountOfPossibleChannelsByTeamType(TeamType.SmallCrossfunctionalTeam)} channel and +{Teams.GetAmountOfPossibleFeaturesByTeamType(TeamType.SmallCrossfunctionalTeam)} feature at time";
    }

    public override string GetButtonTitle()
    {
        return "Create new team";
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

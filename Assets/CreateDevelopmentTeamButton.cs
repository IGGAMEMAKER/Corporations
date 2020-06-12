using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateDevelopmentTeamButton : CompanyUpgradeButton
{
    public override void Execute()
    {
        Teams.AddTeam(Flagship, TeamType.DevelopmentTeam);
    }

    public override string GetBenefits()
    {
        return $"+{Teams.GetAmountOfPossibleFeaturesByTeamType(TeamType.DevelopmentTeam)} feature at time";
        //return "+1 feature at time";
    }

    public override string GetButtonTitle()
    {
        return "Create development team";
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

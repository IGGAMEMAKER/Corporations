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
        return $"+{Teams.GetAmountOfPossibleChannelsByTeamType(TeamType.SmallCrossfunctionalTeam)} channel and +{Teams.GetAmountOfPossibleFeaturesByTeamType(TeamType.SmallCrossfunctionalTeam)} feature at time";
    }

    public override string GetButtonTitle()
    {
        return "Create small universal team";
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

//public class AddTeamButton : CompanyUpgradeButton
//{
//    public TeamType TeamType;
//    public override void Execute()
//    {
//        Teams.AddTeam(Flagship, TeamType);
//    }

//    public override string GetBenefits()
//    {
//        throw new System.NotImplementedException();
//    }

//    public override string GetButtonTitle()
//    {
//        throw new System.NotImplementedException();
//    }

//    public override string GetHint()
//    {
//        throw new System.NotImplementedException();
//    }

//    public override bool GetState()
//    {
//        throw new System.NotImplementedException();
//    }
//}

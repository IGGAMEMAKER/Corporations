using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMarketingTeamButton : CompanyUpgradeButton
{
    public override void Execute()
    {
        Teams.AddTeam(Flagship, TeamType.MarketingTeam);
    }

    public override string GetBenefits()
    {
        return "+1 marketing channel at time";
    }

    public override string GetButtonTitle()
    {
        return "Create marketing team";
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

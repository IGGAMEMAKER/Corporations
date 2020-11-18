using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideNewTeamButtonIfOldOnesAreNotFinished : View
{
    // universals
    public GameObject NewTeamButton;
    public GameObject DevTeamButton;
    public GameObject MarketingTeamButton;
    public GameObject SupportTeamButton;
    public GameObject ServersideTeamButton;

    public void SetEntity(TeamTask t)
    {
        bool canHireSpecialisedTeams = Flagship.team.Teams.Count > 1;
        
        //Draw(NewTeamButton, Teams.IsTaskSuitsTeam(TeamType.SmallCrossfunctionalTeam, t));

        Draw(DevTeamButton,         canHireSpecialisedTeams && t.IsFeatureUpgrade);
        Draw(MarketingTeamButton,   canHireSpecialisedTeams && t.IsMarketingTask);
        Draw(SupportTeamButton,     canHireSpecialisedTeams && t.IsSupportTask && false);
        Draw(ServersideTeamButton,  canHireSpecialisedTeams && t.IsHighloadTask); //  && Teams.IsTaskSuitsTeam(TeamType.DevelopmentTeam, t)
    }

    public override void ViewRender()
    {
        base.ViewRender();

        bool canHireSpecialisedTeams = Flagship.team.Teams.Count > 1;

        Draw(DevTeamButton,         canHireSpecialisedTeams);
        Draw(MarketingTeamButton,   canHireSpecialisedTeams);
        Draw(SupportTeamButton,     false);
        Draw(ServersideTeamButton,  canHireSpecialisedTeams); //  && Teams.IsTaskSuitsTeam(TeamType.DevelopmentTeam, t)
    }
}

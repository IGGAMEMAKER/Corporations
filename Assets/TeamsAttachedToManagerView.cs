using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamsAttachedToManagerView : View
{
    public Text AmountOfTeams;
    public TeamType TeamType;
    public RemoveTeamController RemoveTeam;

    public override void ViewRender()
    {
        base.ViewRender();

        var teamType = TeamType;

        var company = Flagship;

        var amountOfTeams = company.team.Teams.ContainsKey(teamType) ? company.team.Teams[teamType] : 0;

        AmountOfTeams.text = "";

        if (amountOfTeams > 0)
        {
            AmountOfTeams.text = $"<b>{amountOfTeams}</b>\n{teamType}";
        }

        Draw(RemoveTeam, amountOfTeams > 0);
        RemoveTeam.TeamType = teamType;
    }
}

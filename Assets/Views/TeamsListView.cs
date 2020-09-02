using Assets;
using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TeamsListView : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        var teamInfo = (TeamInfo)(object)entity;

        var teamId = Flagship.team.Teams.FindIndex(team => team.Name == teamInfo.Name);

        t.GetComponent<TeamView>().SetEntity(teamInfo, teamId, true);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var company = Flagship;
        var teams = company.team.Teams;

        SetItems(teams.OrderByDescending(t => 4 - t.Tasks.Count));
    }

    public override void OnItemSelected(int ind)
    {
        base.OnItemSelected(ind);

        var teamId = Items[ind].GetComponent<TeamView>().teamId;

        var relay = FindObjectOfType<FlagshipRelayInCompanyView>();

        var task = relay.TeamTask;
        Teams.AddTeamTask(Flagship, Q, teamId, task);

        relay.ChooseMainScreen();

        if (task.IsFeatureUpgrade)
        {
            SoundManager.Play(Sound.ProgrammingTask);
        }
    }
}

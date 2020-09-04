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

        var relay = FindObjectOfType<FlagshipRelayInCompanyView>();

        var task = relay.TeamTask;

        var teams = company.team.Teams.Where(t => Teams.SupportsTeamTask(t.TeamType, task));

        SetItems(teams.OrderByDescending(t => 4 - t.Tasks.Count));
    }

    public override void OnItemSelected(int ind)
    {
        base.OnItemSelected(ind);

        var teamId = Items[ind].GetComponent<TeamView>().teamId;

        bool overloaded = Flagship.team.Teams[teamId].Tasks.Count >= 4;

        if (overloaded)
        {
            NotificationUtils.AddSimplePopup(Q, "This team is overloaded", "Choose another team");
            return;
        }

        var relay = FindObjectOfType<FlagshipRelayInCompanyView>();

        var task = relay.TeamTask;
        Teams.AddTeamTask(Flagship, Q, teamId, task);

        relay.ChooseMainScreen();

        if (task.IsFeatureUpgrade)
        {
            SoundManager.Play(Sound.ProgrammingTask);
        }

        if (task.IsHighloadTask)
        {
            SoundManager.Play(Sound.ServerTask);
        }

        if (task.IsMarketingTask)
        {
            SoundManager.Play(Sound.MarketingTask);
        }

        if (task.IsSupportTask)
        {
            SoundManager.Play(Sound.SupportTask);
        }
    }
}

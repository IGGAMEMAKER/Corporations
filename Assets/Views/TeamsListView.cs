using Assets;
using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TeamsListView : ListView
{
    public Text Text1;
    public Text Text2;
    public Text Text3;
    public Text Text4;

    TeamTask task;

    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        var teamInfo = (TeamInfo)(object)entity;

        var teamId = Flagship.team.Teams.FindIndex(team => team.Name == teamInfo.Name);

        t.GetComponent<TeamView>().SetEntity(teamInfo, teamId, task);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var company = Flagship;

        var relay = FindObjectOfType<FlagshipRelayInCompanyView>();

        task = relay.TeamTask;

        var teams = company.team.Teams.Where(t => Teams.IsTaskSuitsTeam(t.TeamType, task));

        RenderTableLabels(task);

        SetItems(teams.OrderByDescending(t => 4 - t.Tasks.Count));
    }

    void RenderLabels(string s1, string s2, string s3, string s4)
    {
        Text1.text = s1;
        Text2.text = s2;
        Text3.text = s3;
        Text4.text = s4;
    }

    void RenderTableLabels(TeamTask task)
    {
        if (task.IsFeatureUpgrade)
        {
            RenderLabels("Feature gain", "Max feature level", "", "Free task slots");
        }

        if (task.IsHighloadTask)
        {
            RenderLabels("", "", "", "Free task slots");
            //RenderLabels("Break chance", "Install time", "", "Free task slots");
        }

        if (task.IsMarketingTask)
        {
            RenderLabels("Marketing Effeciency", "Final gain", "", "Free task slots");
            //RenderLabels("Effeciency", "Final gain", "", "Free task slots");
        }
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

        Refresh();
    }
}

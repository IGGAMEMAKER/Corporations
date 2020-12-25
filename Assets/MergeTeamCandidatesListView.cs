using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Core;
using UnityEngine;

public class MergeTeamCandidatesListView : ListView
{
    public GameObject Label;
    
    public override void SetItem<T>(Transform t, T entity)
    {
        t.GetComponent<TeamPreview>().SetEntity((TeamInfo)(object)entity);
    }

    public override void ViewRender()
    {
        base.ViewRender();
        
        var company = Flagship;
        var teams = company.team.Teams;

        var team = teams[SelectedTeam];

        var candidates = teams.Where(t => Teams.IsCanMergeTeams(company, team, t));
        
        SetItems(candidates);
        
        Draw(Label, candidates.Any());
    }
    
    // public override void OnItemSelected(int ind)
    // {
    //     base.OnItemSelected(ind);
    //
    //     FindObjectOfType<FlagshipRelayInCompanyView>().ChooseManagersTabs(Item.GetComponent<TeamPreview>().TeamInfo.ID);
    //
    //     ScheduleUtils.PauseGame(Q);
    // }
    
    public override void OnItemSelected(int ind)
    {
        base.OnItemSelected(ind);

        ChooseManagersTabs(Item.GetComponent<TeamPreview>().TeamInfo.ID);

        ScheduleUtils.PauseGame(Q);
    }
    
    public void ChooseManagersTabs(int teamId)
    {
        ScreenUtils.SetSelectedTeam(Q, teamId);
        // ShowOnly(ManagersTabs, Tabs);
        //
        // Hide(TaskPanel);

        ScreenUtils.Navigate(Q, ScreenMode.TeamScreen);
    }
}

using System.Collections.Generic;
using System.Linq;
using Assets.Core;
using UnityEngine;

public class TeamsPanelListView : ListView
{
    public override void SetItem<T>(Transform t, T entity)
    {
        t.GetComponent<TeamPreview>().SetEntity((TeamInfo)(object)entity);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var company = Flagship;
        var teams = company.team.Teams;

        var independentTeams = teams.Where(t => t.isIndependentTeam);
        SetItems(independentTeams);
    }

    public override void OnItemSelected(int ind)
    {
        base.OnItemSelected(ind);

        var teamId = Item.GetComponent<TeamPreview>().TeamInfo.ID;
        
        SetParameter(C.MENU_SELECTED_TEAM, teamId);
        ScreenUtils.Navigate(Q, ScreenMode.TeamScreen);
        
        // FindObjectOfType<FlagshipRelayInCompanyView>().ChooseManagersTabs(teamId);

        ScheduleUtils.PauseGame(Q);
    }
}
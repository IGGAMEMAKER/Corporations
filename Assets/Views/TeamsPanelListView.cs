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

        if (CurrentScreen == ScreenMode.TeamScreen)
            SetItems(Teams.GetDependantTeams(teams[SelectedTeam], company));
        else
            SetItems(teams);
    }

    public override void OnItemSelected(int ind)
    {
        base.OnItemSelected(ind);

        FindObjectOfType<FlagshipRelayInCompanyView>().ChooseManagersTabs(ind);

        ScheduleUtils.PauseGame(Q);
    }
}
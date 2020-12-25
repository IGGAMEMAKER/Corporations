using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Core;
using UnityEngine;

public class ParentTeamListView : ListView
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

        if (team.isIndependentTeam)
        {
            SetItems(new List<GameObject>());
            Hide(Label);
            return;
        }

        var parent = teams[team.ParentID];
        
        SetItems(new List<TeamInfo> { parent });
        
        Draw(Label, true);
    }
    
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

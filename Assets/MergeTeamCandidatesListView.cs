using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MergeTeamCandidatesListView : ListView
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

        var team = teams[SelectedTeam];
        
        SetItems(teams.Where(t => t.TeamType == team.TeamType && !t.isCoreTeam && team.Rank == TeamRank.Department));
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Core;
using UnityEngine;

public class DependantTeamsListView : ListView
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
        
        var dependantTeams = Teams.GetDependantTeams(teams[SelectedTeam], company);
        
        SetItems(dependantTeams);
        
        Draw(Label, dependantTeams.Any());
    }
}

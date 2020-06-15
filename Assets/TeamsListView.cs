using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamsListView : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        t.GetComponent<TeamView>().SetEntity((TeamInfo)(object)entity, index);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var company = Flagship;
        var teams = company.team.Teams;

        SetItems(teams);
    }
}

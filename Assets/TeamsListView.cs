using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamsListView : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        t.GetComponent<TeamView>().SetEntity((TeamType)(object)entity, index);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var company = Flagship;
        var s = company.team.Teams;

        var teams = new List<TeamType>();

        foreach (var p in s.Keys)
        {
            for (var i = 0; i < s[p]; i++)
            {
                teams.Add(p);
            }
        }

        SetItems(teams);
    }
}

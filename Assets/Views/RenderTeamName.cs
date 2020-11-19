using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderTeamName : ParameterView
{
    public override string RenderValue()
    {
        var team = Flagship.team.Teams[SelectedTeam];

        return team.Name;
    }

    private void OnEnable()
    {
        ViewRender();
    }
}

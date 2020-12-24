using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTeamPreview : View
{
    private void OnEnable()
    {
        GetComponent<TeamPreview>().SetEntity(Flagship.team.Teams[SelectedTeam]);
    }
}

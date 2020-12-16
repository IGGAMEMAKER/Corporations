using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Core;
using UnityEngine;
using UnityEngine.UI;

public class CandidatesForRoleListView2 : ListView
{
    public Text RoleBenefit;
    
    public override void SetItem<T>(Transform t, T entity)
    {
        t.GetComponent<CandidateForRoleView>().SetEntity(entity as GameEntity);
    }

    private void OnEnable()
    {
        var role = (WorkerRole)GetParameter("role");
        var company = Flagship;
        var team = company.team.Teams[SelectedTeam];

        var managers = Teams.GetCandidatesForTeam(company, team, Q)
            .Select(id => Humans.Get(Q, id))
            .Where(h => Humans.GetRole(h) == role)
            ;
        SetItems(managers);
        
        // ----------
        RoleBenefit.text = Visuals.Positive(Teams.GetRoleDescription(role, Q, true));
    }
}

using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SelectedTeamEmployeesListView : ListView
{
    public override void SetItem<T>(Transform t, T entity)
    {
        var human = entity as GameEntity;
        //var human = (int)(object)entity;

        t.GetComponent<WorkerView>().SetEntity(human.human.Id, Humans.GetRole(human));
        //t.GetComponent<EmployeePreview>().SetEntity(human.human.Id);
    }

    //public override void ViewRender()
    //{
    //    base.ViewRender();

    //    var company = SelectedCompany;

    //    var team = company.team.Teams[SelectedTeam];

    //    SetItems(team.Managers.Select(humanId => Humans.Get(Q, humanId)));
    //}
}

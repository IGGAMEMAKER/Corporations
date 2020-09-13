using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerFocusListView : ListView
{
    public GameObject FocusList;
    public GameObject Organisation;

    public OrganisationView OrganisationView;


    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        t.GetComponent<ManagerTaskSlotView>().SetEntity(index);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        //List<ManagerTask> tasks = new List<ManagerTask> { ManagerTask.Documentation, ManagerTask.Investments, ManagerTask.None };
        List<ManagerTask> tasks = Flagship.team.Teams[SelectedTeam].ManagerTasks;

        SetItems(tasks);
        Hide(FocusList);
    }

    public override void OnItemSelected(int ind)
    {
        base.OnItemSelected(ind);

        Show(FocusList);
        Hide(Organisation);
    }

    public override void OnDeselect()
    {
        base.OnDeselect();

        Hide(FocusList);
        Show(Organisation);

        OrganisationView.ViewRender();
    }

    void SetManagerTask(ManagerTask task)
    {
        Teams.SetManagerTask(Flagship, SelectedTeam, ChosenIndex, task);

        OnDeselect();
        ViewRender();
    }

    public void ChooseRecruitment()
    {
        SetManagerTask(ManagerTask.Recruiting);
    }

    public void ChooseDocumentation()
    {
        SetManagerTask(ManagerTask.Documentation);
    }

    public void ChooseOrganisation()
    {
        SetManagerTask(ManagerTask.Organisation);
    }

    public void ChooseInvestments()
    {
        SetManagerTask(ManagerTask.Investments);
    }

    public void ChooseInternalConflicts()
    {
        SetManagerTask(ManagerTask.ProxyTasks);
    }

    public void ChoosePolishing()
    {
        SetManagerTask(ManagerTask.Polishing);
    }

    public void ChooseViralSpread()
    {
        SetManagerTask(ManagerTask.ViralSpread);
    }
}

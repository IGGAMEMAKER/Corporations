using UnityEngine;

public class AddTeamTaskListView : ListView
{
    public int TeamId;
    public int FreeSlots;

    int TasksCount => Flagship.team.Teams[TeamId].Tasks.Count;

    public void SetEntity(int teamId)
    {
        TeamId = teamId;

        ViewRender();
    }

    public override void SetItem<T>(Transform t, T entity)
    {
        t.GetComponent<AddTaskForTheTeamController>().SetEntity(TeamId);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        if (Flagship.team.Teams.Count > TeamId)
        {
            FreeSlots = Mathf.Max(C.TASKS_PER_TEAM - TasksCount, 0);

            SetItems(new int[FreeSlots]);
        }
    }
}

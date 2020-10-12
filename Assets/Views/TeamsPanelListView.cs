using Assets.Core;
using UnityEngine;

public class TeamsPanelListView : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        t.GetComponent<TeamPreview>().SetEntity((TeamInfo)(object)entity, index);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var company = Flagship;
        var teams = company.team.Teams;

        SetItems(teams);
    }

    public override void OnItemSelected(int ind)
    {
        base.OnItemSelected(ind);

        FindObjectOfType<FlagshipRelayInCompanyView>().ChooseManagersTabs(ind);

        ScheduleUtils.PauseGame(Q);
    }
}
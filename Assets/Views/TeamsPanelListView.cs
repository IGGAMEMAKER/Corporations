using Assets.Core;
using UnityEngine;

public class TeamsPanelListView : ListView
{
    public GameObject TeamButtons;

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

        //FindObjectOfType<MainPanelRelay>().ExpandTeams();
        FindObjectOfType<FlagshipRelayInCompanyView>().ChooseManagersTabs(ind);
        ScheduleUtils.PauseGame(Q);
    }

    public override void OnDeselect()
    {
        base.OnDeselect();

        HideButtons();

        var a = FindObjectOfType<MainPanelRelay>();
        if (a != null)
            a.ShowAudiencesAndInvestors();
    }

    public void HideButtons()
    {
        Hide(TeamButtons);
    }
}
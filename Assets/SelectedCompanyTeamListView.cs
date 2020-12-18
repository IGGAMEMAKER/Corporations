using UnityEngine;

public class SelectedCompanyTeamListView : ListView
{
    public override void SetItem<T>(Transform t, T entity)
    {
        t.GetComponent<TeamPreview>().SetEntity((TeamInfo)(object)entity);
    }

    //public override void ViewRender()
    //{
    //    base.ViewRender();

    //    var company = Flagship;

    //    var teams = company.team.Teams;

    //    SetItems(teams);
    //}
}

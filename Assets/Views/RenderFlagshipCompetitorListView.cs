using Assets.Core;
using System.Linq;
using UnityEngine;

public class RenderFlagshipCompetitorListView : ListView
{
    bool showCompetitors = true;

    public override void SetItem<T>(Transform t, T entity)
    {
        t.GetComponent<CompanyView>().SetEntity(entity as GameEntity);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var competitors = Companies.GetCompetitorsOf(Flagship, Q, true)
            .OrderByDescending(Marketing.GetUsers);

        SetItems(competitors);
    }
}

using Assets.Utils;
using UnityEngine;

public class PerspectiveNichesListView : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        t.GetComponent<NichePreview>().SetNiche(entity as GameEntity);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var perspectiveNiches = NicheUtils.GetPerspectiveNiches(GameContext);

        SetItems(perspectiveNiches);
    }
}
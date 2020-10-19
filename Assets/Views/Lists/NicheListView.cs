using UnityEngine;

public class NicheListView : ListView
{
    public override void SetItem<T>(Transform t, T entity)
    {
        t.GetComponent<NichePreview>().SetNiche(entity as GameEntity);
    }
}

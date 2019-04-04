using UnityEngine;

public class NicheListView : ListView
{
    public override void SetItem(Transform t, GameEntity gameEntity)
    {
        t.GetComponent<NichePreview>().SetNiche(gameEntity);
    }
}

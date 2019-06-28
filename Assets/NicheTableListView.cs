using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NicheTableListView : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        t.GetComponent<NicheTableView>().SetEntity(entity as GameEntity);
    }
}

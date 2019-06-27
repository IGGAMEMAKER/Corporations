using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanySearchListView : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        var e = entity as GameEntity;

        if (t.GetComponent<CompanyTableView>() != null)
            t.GetComponent<CompanyTableView>().SetEntity(e);
    }
}

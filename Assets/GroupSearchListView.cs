using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupSearchListView : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        var e = entity as GameEntity;

        t.GetComponent<CompanyTableView>().SetEntity(e, data);
    }

    private void OnEnable()
    {
        Render();
    }

    public void Render()
    {
        var groups = CompanyUtils.GetGroupCompanies(GameContext);
        var quarterlyFilter = GetComponent<GrowthFilterQuarterly>().enabled;


        SetItems(groups, quarterlyFilter);
    }
}

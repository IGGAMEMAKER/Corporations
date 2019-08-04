using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GroupStatsListView : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        t.GetComponent<CompanyGrowthPreview>().SetEntity(entity as GameEntity);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var daughters = CompanyUtils.GetDaughterCompanies(GameContext, MyCompany.company.Id);

        //daughters.OrderByDescending(d => )

        SetItems(daughters);
    }
}

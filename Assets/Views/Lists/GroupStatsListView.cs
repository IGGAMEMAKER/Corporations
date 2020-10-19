using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GroupStatsListView : ListView
{
    public override void SetItem<T>(Transform t, T entity)
    {
        t.GetComponent<CompanyGrowthPreview>().SetEntity(entity as GameEntity);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var daughters = Companies.GetDaughters(Q, MyCompany)
            //.OrderBy(d => CompanyStatisticsUtils.GetIncomeGrowthAbsolute(d, 12))
            .OrderByDescending(d => Economy.CostOf(d, Q))
            .ToArray();

        SetItems(daughters);
    }
}

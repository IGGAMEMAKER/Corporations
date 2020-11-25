using Assets.Core;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FullShareholdersListView : ListView
{
    public override void SetItem<T>(Transform t, T entity)
    {
        var e = (KeyValuePair<int, BlockOfShares>)(object)entity;

        t.GetComponent<ShareholderView>()
            .SetEntity(e.Key, e.Value);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var shareholders = Companies.GetShareholders(SelectedCompany)
            .OrderByDescending(s => Companies.GetAmountOfShares(Q, SelectedCompany, Companies.GetInvestorById(Q, s.Key)));

        SetItems(shareholders);
    }
}

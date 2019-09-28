using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InvestorsWithSameInterestsListView : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        t.GetComponent<MockText>().SetEntity(entity as string);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var funds = NicheUtils.GetFinancialStructuresWithSameInterests(GameContext, SelectedCompany);

        SetItems(funds.ToArray());
    }
}

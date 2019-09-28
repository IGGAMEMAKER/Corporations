using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InvestorsWithSameInterestsListView : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        var fund = entity as GameEntity;
        t.GetComponent<MockText>().SetEntity(fund.company.Name);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var funds = NicheUtils.GetFinancialStructuresWithSameInterests(GameContext, MyCompany);

        SetItems(funds.ToArray());
    }
}

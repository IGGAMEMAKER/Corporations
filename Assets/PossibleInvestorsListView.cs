using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossibleInvestorsListView : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        var investor = entity as GameEntity;

        t.GetComponent<PossibleInvestor>()
            .SetEntity(investor);
    }

    // Start is called before the first frame update
    void Start()
    {
        var list = CompanyUtils.GetPossibleInvestors(GameContext, SelectedCompany.company.Id);

        SetItems(list);
    }
}

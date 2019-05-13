using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillPossibleInvestorsList : View
    , IAnyDateListener
{
    void IAnyDateListener.OnAnyDate(GameEntity entity, int date)
    {
        Render();
    }

    void OnEnable()
    {
        LazyUpdate(this);

        Render();
    }

    void Render()
    {
        var list = CompanyUtils.GetPossibleInvestors(GameContext, SelectedCompany.company.Id);

        GetComponent<PossibleInvestorsListView>()
            .SetItems(list);
    }
}
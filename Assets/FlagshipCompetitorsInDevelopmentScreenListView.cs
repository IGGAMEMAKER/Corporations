using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagshipCompetitorsInDevelopmentScreenListView : ListView
{
    public override void SetItem<T>(Transform t, T entity)
    {
        t.GetComponent<CompanyPreviewView>().SetEntity(entity as GameEntity);
    }

    public override void ViewRender()
    {
        var competitors = Companies.GetDirectCompetitors(Flagship, Q, true);

        SetItems(competitors);
    }
}

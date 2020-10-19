using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositioningVariantsListView2 : ListView
{
    public override void SetItem<T>(Transform t, T entity)
    {
        t.GetComponent<ChangePositioningButton>().SetEntity(index);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var positionings = Marketing.GetNichePositionings(Flagship);

        SetItems(positionings);
    }
}

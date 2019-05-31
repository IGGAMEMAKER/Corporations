using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillSharesList : View
{
    public override void ViewRender()
    {
        base.ViewRender();

        GetComponent<ShareholdersListView>().SetItems(GetInvestments());
    }

    private GameEntity[] GetInvestments()
    {
        return new GameEntity[0];
    }
}

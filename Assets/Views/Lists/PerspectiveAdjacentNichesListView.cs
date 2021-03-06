﻿using Assets.Core;
using UnityEngine;

public class PerspectiveAdjacentNichesListView : ListView
{
    public override void SetItem<T>(Transform t, T entity)
    {
        t.GetComponent<NichePreview>().SetNiche(entity as GameEntity);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var perspectiveNiches = Markets.GetPerspectiveAdjacentNiches(Q, MyCompany);

        SetItems(perspectiveNiches);
    }
}
﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FillAcquisitionShareholders : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        t.GetComponent<VotingShareholderView>().SetEntity((int)(object)entity);
    }

    void Render()
    {
        SetItems(SelectedCompany.shareholders.Shareholders.Keys.ToArray());
    }

    public override void ViewRender()
    {
        base.ViewRender();

        Render();
    }
}

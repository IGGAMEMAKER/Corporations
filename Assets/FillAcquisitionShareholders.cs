using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FillAcquisitionShareholders : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {

        Debug.Log("FillAcquisitionShareholders ");
    }

    private void OnEnable()
    {
        
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

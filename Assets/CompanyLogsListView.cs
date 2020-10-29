using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanyLogsListView : ListView
{
    public override void SetItem<T>(Transform t, T entity)
    {
        t.GetComponent<MockText>().SetEntity((string)(object)entity);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        SetItems(SelectedCompany.logging.Logs);
    }
}

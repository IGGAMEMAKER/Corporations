using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        var allLogs = SelectedCompany.hasLogging ? SelectedCompany.logging.Logs : new List<string>();

        SetItems(allLogs.Select((l, i) => new { l, i }).Where(a => a.i > allLogs.Count - 10).Select(a => a.l));
    }
}

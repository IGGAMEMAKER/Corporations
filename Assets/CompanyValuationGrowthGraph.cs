using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CompanyValuationGrowthGraph : View
{
    public override void ViewRender()
    {
        base.ViewRender();

        var metrics = SelectedCompany.metricsHistory.Metrics;

        var Ys = metrics.Select(m => m.Valuation).ToList();
        var Xs = metrics.Select(m => m.Date).ToList();

        GetComponent<SetGraphData>().SetData(Xs, Ys);
    }
}

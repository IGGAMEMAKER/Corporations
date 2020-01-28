using Assets.Core;
using Assets.Core.Formatting;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SetGroupRecentValuation : View
{
    public override void ViewRender()
    {
        base.ViewRender();

        var metrics = MyCompany.metricsHistory.Metrics.Where(m => CompanyStatisticsUtils.GetLastCalendarYearMetrics(m, CurrentIntDate));

        var xs = metrics.Select(m => m.Date).ToList();
        var ys = metrics.Select(m => m.Valuation).ToList();

        GraphData graphData = new GraphData
        {
            Color = Visuals.GetColorFromString(Colors.COLOR_GOLD),
            Name = "",
            values = ys
        };

        GetComponent<SetGraphData>().SetData(xs, graphData);
    }
}

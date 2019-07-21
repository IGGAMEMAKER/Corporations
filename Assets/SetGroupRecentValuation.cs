using Assets.Utils;
using Assets.Utils.Formatting;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SetGroupRecentValuation : View
{
    public override void ViewRender()
    {
        base.ViewRender();

        var metrics = MyCompany.metricsHistory.Metrics.Where(m => GetLastCalendarYearMetrics(m, CurrentIntDate));

        var xs = metrics.Select(m => m.Date).ToList();
        var ys = metrics.Select(m => m.Valuation).ToList();

        GraphData graphData = new GraphData
        {
            Color = Visuals.Color(VisualConstants.COLOR_GOLD),
            Name = "",
            values = ys
        };

        GetComponent<SetGraphData>().SetData(xs, graphData);
    }

    public static bool GetLastYearMetrics(MetricsInfo metricsInfo, int currentDate)
    {
        return metricsInfo.Date > currentDate - 360;
    }

    public static bool GetLastCalendarYearMetrics(MetricsInfo metricsInfo, int currentDate)
    {
        var year = GetYear(currentDate);
        var date = metricsInfo.Date;

        return date > year * 360 && date <= currentDate;
    }

    public static bool GetLastCalendarQuarterMetrics(MetricsInfo metricsInfo, int currentDate)
    {
        var year = GetYear(currentDate);
        var quarter = (currentDate - year * 360) % 90;

        var date = metricsInfo.Date;

        return date > year * 360 + quarter * 90 && date <= currentDate;
    }

    static int GetYear(int currentDate)
    {
        return currentDate % 360;
    }
}

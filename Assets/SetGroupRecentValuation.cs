using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SetGroupRecentValuation : View
{
    public override void ViewRender()
    {
        base.ViewRender();

        var metrics = MyCompany.metricsHistory.Metrics.Where(m => m.Date > CurrentIntDate - 12 * 30);

        GetComponent<SetGraphData>().SetData(
            metrics.Select(m => m.Date).ToList(),
            metrics.Select(m => m.Valuation).ToList()
            );
    }
}

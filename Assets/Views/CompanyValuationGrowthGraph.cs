using Assets.Core;
using System.Linq;

public class CompanyValuationGrowthGraph : View
{
    public override void ViewRender()
    {
        base.ViewRender();

        var metrics = SelectedCompany.metricsHistory.Metrics;

        var Ys = metrics.Select(m => m.Valuation).ToList();
        var Xs = metrics.Select(m => m.Date).ToList();

        GraphData graphData = new GraphData
        {
            Color = Visuals.GetColorFromString(Colors.COLOR_GOLD),
            Name = "",
            values = Ys
        };

        GetComponent<SetGraphData>().SetData(Xs, graphData);
    }
}

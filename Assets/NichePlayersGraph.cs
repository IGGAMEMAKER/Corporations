using Assets.Utils;
using System.Linq;

public class NichePlayersGraph : View
{
    public override void ViewRender()
    {
        base.ViewRender();

        var metrics = SelectedCompany.metricsHistory.Metrics;

        var Ys = metrics.Select(m => m.Valuation).ToList();
        var Xs = metrics.Select(m => m.Date).ToList();

        GraphData graphData = new GraphData
        {
            Color = Visuals.Color(VisualConstants.COLOR_GOLD),
            Name = "",
            values = Ys
        };

        GetComponent<SetGraphData>().SetData(Xs, graphData);
    }
}

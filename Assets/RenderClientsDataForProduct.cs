using Assets.Core;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RenderClientsDataForProduct : View
{
    public override void ViewRender()
    {
        base.ViewRender();

        var offset = 90;

        var startDate = CurrentIntDate - offset;
        var endDate = CurrentIntDate;


        List<int> xs = new List<int>();

        for (var i = startDate; i < endDate; i+= Balance.PERIOD) // 7 - period length
            xs.Add(i);

        //Debug.Log("amount of dots: " + xs.Count);

        var products = Markets.GetProductsOnMarket(Q, SelectedCompany);
        GraphData[] ys = products
            .Select(p =>
                new GraphData
                {
                    Color = Companies.GetCompanyUniqueColor(p.company.Id),
                    Name = p.company.Name,
                    values = NormalizeGraphData(p, startDate, endDate, xs.Count)
                }
            )
            .ToArray();

        // add current value
        xs.Add(CurrentIntDate + 1);

        GetComponent<SetGraphData>().SetData(xs, ys);
    }

    MetricsInfo GetMetricMockup(long audience, int date) => new MetricsInfo
    {
        AudienceSize = audience,
        Date = date,
        Income = 0,
        Profit = 0,
        Valuation = 0,
    };

    List<long> NormalizeGraphData(GameEntity p, int start, int end, int necessaryMetrics)
    {
        var metrics = p.metricsHistory.Metrics
                        .Where(m => m.Date >= start)
                        .ToList();

        //var necessaryMetrics = (end - start) / Constants.PERIOD; // metrics are saved each 7 days
        var metricsCount = metrics.Count();

        //Debug.Log($"Found {metricsCount} / {necessaryMetrics} queries for {p.company.Name}");

        var insertBefore = necessaryMetrics - metricsCount;
        for (var i = 0; i < insertBefore; i++)
        {
            metrics.Add(GetMetricMockup(0, start + i * Balance.PERIOD));
        }

        // add current value
        metrics.Add(GetMetricMockup(Marketing.GetClients(p), CurrentIntDate + 1));

        var result = metrics.OrderBy(m => m.Date);

        //var loggedMetrics = result.Select(m => $"date={m.Date}, audience={Format.Minify(m.AudienceSize)}");

        //Debug.Log("normalising " + p.company.Name + $" (Entity{p.creationIndex}: {result.Count()}/{necessaryMetrics} " + string.Join("\n", loggedMetrics));

        return result
            .Select(m => m.AudienceSize)
            .ToList();
    }
}

using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RenderClientsDataForProduct : View
{
    public override void ViewRender()
    {
        base.ViewRender();

        var product = SelectedCompany;
        var history = product.metricsHistory.Metrics;

        var months = 12*4;

        var start1 = Mathf.Max(history.Count - months, 0);
        var count = history.Count - start1;

        if (count > months)
            count = months;

        var metrics = history.GetRange(start1, count);


        List<int> xs = metrics.Select(m => m.Date).ToList();

        // add current value
        xs.Add(CurrentIntDate);

        var start = xs[0];

        var end = xs[xs.Count - 1];


        GraphData[] ys = new GraphData[1];
        ys[0] = new GraphData
        {
            Color = Companies.GetCompanyUniqueColor(product.company.Id),
            Name = product.company.Name,
            values = metrics.Select(m => m.AudienceSize).ToList()
        };

        // add current value
        ys[0].values.Add(MarketingUtils.GetClients(product));

        //var products = new GameEntity[] { product }; // Markets.GetProductsOnMarket(GameContext, SelectedCompany);
        //GraphData[] ys = products
        //    .Select(p =>
        //        new GraphData {
        //            Color = Companies.GetCompanyUniqueColor(p.company.Id),
        //            Name = p.company.Name,
        //            values = p.metricsHistory.Metrics
        //                .Where(m => m.Date >= start && m.Date <= end)
        //                .Select(m => m.AudienceSize)
        //                .ToList()
        //        }
        //    )
        //    .ToArray();
        

        GetComponent<SetGraphData>().SetData(xs, ys);
    }

    //List<long> NormalizeGraphData (List<MetricsInfo> metrics, int start, int end)
    //{
    //    var insertBefore = 0;
    //    var insertAfter = 0;



    //    for (int i = start; i < end; i++)
    //    {

    //    }
    //    metrics.Insert()
    //}
}

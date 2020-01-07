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

        var metrics = SelectedCompany.metricsHistory.Metrics;
        List<int> xs = metrics.Select(m => m.Date).ToList();

        var start = xs[0];
        var end = xs[xs.Count - 1];


        //GraphData[] ys = new GraphData[1];
        //ys[0] = new GraphData
        //{
        //    Color = Companies.GetCompanyUniqueColor(SelectedCompany.company.Id),
        //    Name = "qwe",
        //    values = metrics.Select(m => m.AudienceSize).ToList()
        //};

        var products = new GameEntity[] { SelectedCompany }; // Markets.GetProductsOnMarket(GameContext, SelectedCompany);
        GraphData[] ys = products
            .Select(p =>
                new GraphData {
                    Color = Companies.GetCompanyUniqueColor(p.company.Id),
                    Name = p.company.Name,
                    values = p.metricsHistory.Metrics
                        .Where(m => m.Date >= start && m.Date <= end)
                        .Select(m => m.AudienceSize)
                        .ToList()
                }
            )
            .ToArray();
        

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

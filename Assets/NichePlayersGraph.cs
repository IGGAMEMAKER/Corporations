using Assets.Utils;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NichePlayersGraph : View
{
    public override void ViewRender()
    {
        base.ViewRender();

        var players = NicheUtils.GetProductsOnMarket(GameContext, SelectedNiche, true);

        var Xs = GetLast12Months();

        var graphDatas = players
            .Select(
                c => new GraphData {
                    Color = GetCompanyColor(c),
                    Name = $"{c.company.Name}, clients: ",

                    values = FillListWithZeroesIfNotEnoughData(GetLastYearMetrics(c), c, Xs.Count)
                });

        GetComponent<SetGraphData>().SetData(Xs, graphDatas.ToArray());
    }

    Color GetCompanyColor(GameEntity c)
    {
        var id = c.company.Id;

        return CompanyUtils.GetCompanyUniqueColor(id);
    }

    List<long> GetLastYearMetrics(GameEntity c)
    {
        return c.metricsHistory.Metrics
                    .Where(m => CompanyStatisticsUtils.GetLastYearMetrics(m, CurrentIntDate))
                    .Select(m => m.AudienceSize)
                    .ToList();
    }

    List<long> FillListWithZeroesIfNotEnoughData(List<long> list, GameEntity c, int amountOfData)
    {
        while (list.Count < amountOfData)
        {
            if (c.isAlive)
                list.Add(0);
            else
                list.Insert(0, 0);
        }

        return list;
    }

    List<int> GetLast12Months()
    {
        var list = new List<int>();

        var month = CompanyStatisticsUtils.GetTotalMonth(CurrentIntDate);

        for (var i = Mathf.Max(0, month - 12); i < month; i++)
            list.Add(i);

        return list;
    }
}

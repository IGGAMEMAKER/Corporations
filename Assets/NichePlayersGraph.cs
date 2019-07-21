using Assets.Utils;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NichePlayersGraph : View
{
    public override void ViewRender()
    {
        base.ViewRender();

        var players = NicheUtils.GetPlayersOnMarket(GameContext, SelectedNiche, true);

        var Xs = GetLast12Months();

        var graphDatas = players
            .Select(
                c => new GraphData {
                    Color = GetCompanyColor(c),
                    Name = $"{c.company.Name}, clients: ",

                    values = FillListWithZeroesIfNotEnoughData(GetLastYearMetrics(c), Xs.Count)
                });

        GetComponent<SetGraphData>().SetData(Xs, graphDatas.ToArray());
    }

    Color GetCompanyColor(GameEntity c)
    {
        var id = c.company.Id + 1;

        return CompanyUtils.GetCompanyUniqueColor(id);
    }

    List<long> GetLastYearMetrics(GameEntity c)
    {
        return c.metricsHistory.Metrics
                    .Where(m => CompanyStatisticsUtils.GetLastYearMetrics(m, CurrentIntDate))
                    .Select(m => m.AudienceSize)
                    .ToList();
    }

    List<long> FillListWithZeroesIfNotEnoughData(List<long> list, int amountOfData)
    {
        while (list.Count < amountOfData)
            list.Add(0);

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

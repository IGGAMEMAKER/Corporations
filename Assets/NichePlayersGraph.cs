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
                    Color = Visuals.Color(VisualConstants.COLOR_GOLD),
                    Name = c.company.Name,
                    values = FillListWithZeroesIfNotEnoughData(
                        c.metricsHistory.Metrics
                            .Where(m => CompanyStatisticsUtils.GetLastCalendarYearMetrics(m, CurrentIntDate))
                            .Select(m => m.AudienceSize)
                            .ToList(),
                        Xs.Count)
                });


        GetComponent<SetGraphData>().SetData(Xs, graphDatas.ToArray());
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
        {
            list.Add(0);
        }

        return list;
    }
}

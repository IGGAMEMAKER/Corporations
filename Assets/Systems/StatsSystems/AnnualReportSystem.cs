using Assets.Core;
using Entitas;
using System.Collections.Generic;
using System.Linq;

public class AnnualReportSystem : OnYearChange
{
    public AnnualReportSystem(Contexts contexts) : base(contexts) { }

    protected override void Execute(List<GameEntity> entities)
    {
        var reportContainer = gameContext.GetEntities(GameMatcher.Reports)[0];

        var reports = reportContainer.reports.AnnualReports;

        int date = ScheduleUtils.GetCurrentDate(gameContext);

        AnnualReport report = new AnnualReport
        {
            Groups = GetGroupList(),
            People = GetPeopleList(),
            Products = GetProductList(),
            Date = date
        };

        reports.Add(report);

        reportContainer.ReplaceReports(reports);
    }

    List<ReportData> WrapIndices(List<ReportData> datas)
    {
        for (var i = 0; i < datas.Count; i++)
        {
            datas[i].position = i;
        }

        return datas.ToList();
    }

    List<ReportData> GetProductList()
    {
        var products = Companies.GetProductCompanies(gameContext)
            .OrderByDescending(p => Economy.CostOf(p, gameContext));


        var List = products.Select(p => new ReportData { Cost = Economy.CostOf(p, gameContext), ShareholderId = p.company.Id }).ToList();

        return WrapIndices(List);
    }

    List<ReportData> GetGroupList()
    {
        var groups = Companies.GetGroupCompanies(gameContext)
            .OrderByDescending(g => Economy.CostOf(g, gameContext))
            .ToArray();

        var List = groups.Select(g => new ReportData { Cost = Economy.CostOf(g, gameContext), ShareholderId = g.shareholder.Id }).ToList();
        
        return WrapIndices(List);
    }

    List<ReportData> GetPeopleList()
    {
        var people = Investments.GetInfluencialPeople(gameContext);

        var List = new List<ReportData>();
        
        for (var i = 0; i < people.Length; i++)
        {
            var h = people[i];

            List.Add(new ReportData
            {
                Cost = Investments.GetInvestorCapitalCost(gameContext, h),
                ShareholderId = h.shareholder.Id,
                position = i
            });
        }

        return List;
    }

    private void SaveGroupCompanyMetrics(GameEntity e, int date)
    {
        CompanyStatisticsUtils.AddMetrics(gameContext, e, new MetricsInfo
        {
            AudienceSize = 0,
            Date = date,
            Income = Economy.GetCompanyIncome(gameContext, e),
            Profit = Economy.GetProfit(gameContext, e),
            Valuation = Economy.CostOf(e, gameContext)
        });
    }
}

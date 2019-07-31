using Assets.Utils;
using Entitas;
using System.Collections.Generic;
using System.Linq;

class AnnualReportSystem : OnYearChange, IInitializeSystem
{
    public AnnualReportSystem(Contexts contexts) : base(contexts)
    {

    }

    protected override void Execute(List<GameEntity> entities)
    {
        var reportContainer = gameContext.GetEntities(GameMatcher.Reports)[0];

        var reports = reportContainer.reports.AnnualReports;

        int date = ScheduleUtils.GetCurrentDate(gameContext);

        AnnualReport report = new AnnualReport
        {
            Groups = GetGroupList(),
            People = GetPeopleList(),
            Products = GetGroupList(),
            Date = date
        };

        reports.Add(report);

        reportContainer.ReplaceReports(reports);
    }

    List<ReportData> GetGroupList()
    {
        var groups = CompanyUtils.GetGroupCompanies(gameContext)
            .OrderByDescending(g => CompanyEconomyUtils.GetCompanyCost(gameContext, g.company.Id))
            .ToArray();

        var List = new List<ReportData>();
        
        for (var i = 0; i < groups.Length; i++)
        {
            var g = groups[i];

            List.Add(new ReportData
            {
                Cost = CompanyEconomyUtils.GetCompanyCost(gameContext, g.company.Id),
                ShareholderId = g.shareholder.Id,
                position = i
            });
        }

        return List;
    }

    List<ReportData> GetPeopleList()
    {
        var people = InvestmentUtils.GetInfluencialPeople(gameContext);

        var List = new List<ReportData>();
        
        for (var i = 0; i < people.Length; i++)
        {
            var h = people[i];

            List.Add(new ReportData
            {
                Cost = InvestmentUtils.GetInvestorCapitalCost(gameContext, h),
                ShareholderId = h.shareholder.Id,
                position = i
            });
        }

        return List;
    }

    void IInitializeSystem.Initialize()
    {
        var e = gameContext.CreateEntity();

        e.AddReports(new List<AnnualReport>());
    }

    private void SaveGroupCompanyMetrics(GameEntity e, int date)
    {
        CompanyStatisticsUtils.AddMetrics(gameContext, e.company.Id, new MetricsInfo
        {
            AudienceSize = 0,
            Date = date,
            Income = CompanyEconomyUtils.GetCompanyIncome(e, gameContext),
            Profit = CompanyEconomyUtils.GetBalanceChange(e, gameContext),
            Valuation = CompanyEconomyUtils.GetCompanyCost(gameContext, e.company.Id)
        });
    }
}

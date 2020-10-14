using Entitas;
using System.Collections.Generic;

public struct MetricsInfo
{
    public int Date;
    public long Income;
    public long Valuation;

    // balance change
    public long Profit;
    public long AudienceSize;
}

public class ReportData
{
    public long Cost;
    public int ShareholderId;
    public int position;
}

public struct AnnualReport
{
    public List<ReportData> People;
    public List<ReportData> Groups;
    public List<ReportData> Products;
    public int Date;
}


[Game]
public class MetricsHistoryComponent : IComponent
{
    public List<MetricsInfo> Metrics;
}

public class ReportsComponent : IComponent
{
    public List<AnnualReport> AnnualReports;
}
using Assets.Core;

public class RenderCampaignStats : UpgradedParameterView
{
    public override string RenderHint() => "";

    public override string RenderValue()
    {
        var menu = ScheduleUtils.GetCampaignStats(Q);
        var stats = menu.Stats;

        var companies = Companies.Get(Q).Length;

        return $"Acquisitions: {stats[CampaignStat.Acquisitions]}" +
            $"\n\nBankruptcies: {stats[CampaignStat.Bankruptcies]}" +
            $"\n\nFunds: {stats[CampaignStat.SpawnedFunds]}" +
            $"\n\nCompanies: {companies}" +
            //$"\n\nManagers: 300" +
            $"\n\nGrown UP companies: {stats[CampaignStat.PromotedCompanies]}"
            ;
    }
}

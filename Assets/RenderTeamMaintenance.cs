using Assets.Utils;

public class RenderTeamMaintenance : UpgradedParameterView
{
    long Maintenance
    {
        get
        {
            return CompanyEconomyUtils.GetCompanyMaintenance(GameContext, SelectedCompany.company.Id);
        }
    }

    public override string RenderHint()
    {
        var e = SelectedCompany;

        var universals = CompanyEconomyUtils.GetUniversalsMaintenance(e);
        var programmers = CompanyEconomyUtils.GetProgrammersMaintenance(e);
        var marketers = CompanyEconomyUtils.GetMarketersMaintenance(e);
        var managers = CompanyEconomyUtils.GetManagersMaintenance(e);
        var topManagers = CompanyEconomyUtils.GetTopManagersMaintenance(e);

        var CEO = CompanyEconomyUtils.GetCEOMaintenance(e);

        var bonus = new BonusContainer("Maintenance")
            .AppendAndHideIfZero("CEO", CEO)
            .AppendAndHideIfZero("Top Management", topManagers)
            .AppendAndHideIfZero("Universals", universals)
            .AppendAndHideIfZero("Programmers", programmers)
            .AppendAndHideIfZero("Marketers", marketers)
            .AppendAndHideIfZero("Managers", managers)
            ;

        return bonus.ToString(true);
    }

    public override string RenderValue()
    {
        return Visuals.Negative($"${ValueFormatter.Shorten(Maintenance)}");
    }
}

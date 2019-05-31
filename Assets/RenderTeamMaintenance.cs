using Assets.Utils;

public class RenderTeamMaintenance : UpgradedParameterView
{
    long Maintenance
    {
        get
        {
            return CompanyEconomyUtils.GetCompanyMaintenance(GameContext, MyProductEntity.company.Id);
        }
    }

    public override string RenderHint()
    {
        var e = MyProductEntity;

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

        return bonus.ToString();
    }

    public override string RenderValue()
    {
        return $"${ValueFormatter.Shorten(Maintenance)}";
    }
}

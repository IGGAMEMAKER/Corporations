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
        var company = MyProductEntity;

        var programmers = TeamUtils.GetProgrammers(company);
        var managers = TeamUtils.GetManagers(company);
        var marketers = TeamUtils.GetMarketers(company);
        var universals = TeamUtils.GetUniversals(company);
        var topManagers = TeamUtils.GetTopManagers(company);

        var bonus = new BonusContainer("Maintenance")
            .AppendAndHideIfZero("CEO", 0)
            .AppendAndHideIfZero("Universals", universals)
            .AppendAndHideIfZero("Programmers", programmers)
            .AppendAndHideIfZero("Marketers", marketers)
            .AppendAndHideIfZero("Top Management", topManagers)
            .AppendAndHideIfZero("Managers", managers)
            ;

        return bonus.ToString();
    }

    public override string RenderValue()
    {
        return $"${ValueFormatter.Shorten(Maintenance)}";
    }
}

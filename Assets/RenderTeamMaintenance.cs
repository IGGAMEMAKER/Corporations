using Assets.Utils;

public class RenderTeamMaintenance : UpgradedParameterView
{
    long maintenance
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
            .Append("CEO", 0)
            .AppendAndHideIfZero("Universals", universals)
            .Append("Programmers", programmers)
            .Append("Marketers", marketers)
            .AppendAndHideIfZero("Managers", managers)
            .AppendAndHideIfZero("Top Management", topManagers);

        return bonus.ToString();
    }

    public override string RenderValue()
    {
        return $"${ValueFormatter.Shorten(maintenance)}";
    }
}

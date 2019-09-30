using Assets.Utils;

public class RenderTeamMaintenance : UpgradedParameterView
{
    long Maintenance
    {
        get
        {
            return EconomyUtils.GetCompanyMaintenance(GameContext, SelectedCompany.company.Id);
        }
    }

    public override string RenderHint()
    {
        var e = SelectedCompany;

        var universals = EconomyUtils.GetUniversalsMaintenance(e);
        var programmers = EconomyUtils.GetProgrammersMaintenance(e);
        var marketers = EconomyUtils.GetMarketersMaintenance(e);
        var managers = EconomyUtils.GetManagersMaintenance(e);
        var topManagers = EconomyUtils.GetTopManagersMaintenance(e);

        var CEO = EconomyUtils.GetCEOMaintenance(e);

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
        return Visuals.Negative($"${Format.Minify(Maintenance)}");
    }
}

using Assets.Utils;

public class RenderTeamMaintenance : UpgradedParameterView
{
    long Maintenance
    {
        get
        {
            return Economy.GetCompanyMaintenance(GameContext, SelectedCompany.company.Id);
        }
    }

    public override string RenderHint()
    {
        return "NO TEAM MAINTENANCE HINT";
    }

    public override string RenderValue()
    {
        return Visuals.Negative($"${Format.Minify(Maintenance)}");
    }
}

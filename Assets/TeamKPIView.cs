using Assets.Core;

public class TeamKPIView : ParameterView
{
    public override string RenderValue()
    {
        string text = "";

        var product = Flagship;

        var team = product.team.Teams[SelectedTeam];

        var marketingEff = Teams.GetMarketingTeamEfficiency(Q, product, team) / 100;
        var devEff = Teams.GetDevelopmentTeamEfficiency(Q, product, team) / 100;

        var featureCap = Teams.GetFeatureRatingCap(product, team, Q);

        var featureMockup = Teams.GetDevelopmentTaskMockup();
        var marketingMockup = Teams.GetMarketingTaskMockup();
        var serverMockup = new TeamTaskSupportFeature(new SupportFeature { SupportBonus = new SupportBonus(5_000_000) });

        var devSlots = Teams.GetSlotsForTask(team, featureMockup);
        var markSlots = Teams.GetSlotsForTask(team, marketingMockup);
        var serverSlots = Teams.GetSlotsForTask(team, serverMockup);

        if (Teams.IsTaskSuitsTeam(team.TeamType, marketingMockup))
        {
            text += $"Marketing efficiency: {Visuals.Colorize(marketingEff, 50, 300)}%";
        }

        if (Teams.IsTaskSuitsTeam(team.TeamType, featureMockup))
        {
            var cap = featureCap.Sum();
            var gradientColor = Visuals.GetGradientColor(0, 10, cap);

            text += $"\nDevelopment speed: {Visuals.Colorize(devEff, 50, 250)}%";
            text += $"\nMax feature cap: {Visuals.Colorize(cap.ToString("0.0"), gradientColor)}lvl due to {0}";
        }

        //text += $"\n\nThis team gives: {1} marketing, {1} development and {1} server slots";
        //text += $"\n\nThis team gives:\n";
        text += $"\n\n";

        if (devSlots > 0)
            text += $"{Visuals.Positive("+" + devSlots)} development ";

        if (markSlots > 0)
            text += $"{Visuals.Positive("+" + markSlots)} marketing ";

        if (serverSlots > 0)
            text += $"{Visuals.Positive("+" + serverSlots)} server ";

        text += "\nslots";


        return text;
    }
}

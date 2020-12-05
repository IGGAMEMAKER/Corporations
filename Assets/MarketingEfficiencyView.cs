using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MarketingEfficiencyView : UpgradedParameterView
{
    public override string RenderHint()
    {
        var product = Flagship;

        var eff = Teams.GetMarketingEfficiency(product);

        var marketingOrientedTeams = product.team.Teams.Where(t => Teams.IsTaskSuitsTeam(t.TeamType, Teams.GetMarketingTaskMockup()));

        var effs = string.Join("\n", marketingOrientedTeams.Select(m => $"* {m.Name} ({Visuals.Colorize(Teams.GetMarketingTeamEfficiency(Q, Flagship, m) / 100)}%)"));

        var competitionPhrase = Visuals.Positive("NO COMPETITORS: X2\n\n");

        if (!product.teamEfficiency.Efficiency.isUniqueCompany)
            competitionPhrase = "";

        return $"Average marketing efficiency is: {eff}%\n\n{effs}\n\n{competitionPhrase}Hire better marketing leads to increase this value";
    }

    public override string RenderValue()
    {
        Teams.UpdateTeamEfficiency(Flagship, Q);

        var eff = Teams.GetMarketingEfficiency(Flagship);

        Colorize(eff, 0, 100);

        return eff + "%";
    }
}

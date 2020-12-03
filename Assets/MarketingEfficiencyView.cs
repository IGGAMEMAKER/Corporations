using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MarketingEfficiencyView : UpgradedParameterView
{
    public override string RenderHint()
    {
        var eff = Teams.GetMarketingEfficiency(Flagship);

        var marketingOrientedTeams = Flagship.team.Teams.Where(t => Teams.IsTaskSuitsTeam(t.TeamType, Teams.GetMarketingTaskMockup()));

        var effs = string.Join("\n", marketingOrientedTeams.Select(m => $"{m.Name} ({Visuals.Colorize(Teams.GetMarketingTeamEffeciency(Q, Flagship, m))}%)"));

        return $"Average marketing efficiency is: {eff}%\n\n{effs}\n\nHire better marketing leads to increase this value";
    }

    public override string RenderValue()
    {
        var eff = Teams.GetMarketingEfficiency(Flagship);

        Colorize(eff, 0, 100);

        return eff + "%";
    }
}

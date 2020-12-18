using System.Collections;
using System.Collections.Generic;
using Assets.Core;
using UnityEngine;

public class ManagerPointsView2 : UpgradedParameterView
{
    public override string RenderValue()
    {
        var company = MyCompany;
        
        var efficiency = Companies.GetManagementEfficiency(company, Q);

        var change = GetManagerPointChange(Flagship);
        var changeSum = (int) change.Sum();

        Colorize(efficiency, 0, 100);

        return efficiency + $" ({Visuals.Colorize(changeSum)})";
    }

    public override string RenderHint()
    {
        var change = GetManagerPointChange(Flagship);
        
        return change.ToString();
    }

    public static Bonus<float> GetManagerPointChange(GameEntity company)
    {
        var teams = company.team.Teams;
        
        var bonus = new Bonus<float>("Point gain");

        bool teamsOnly = teams.Count > 3;

        foreach (var team in teams)
        {
            var roles = Teams.GetRolesForTeam(team);
            var teamPoints = 0f;
            
            foreach (var role in roles)
            {
                var manager = Teams.GetWorkerByRole(role, team, Q);
                var leadingBonus = Teams.GetMainManagerRole(team) == role ? 2 : 1;
                var coreTeamBonus = team.isCoreTeam ? 3 : 1;

                var rating = Teams.GetEffectiveManagerRating(Q, company, manager);
                var gain = leadingBonus * coreTeamBonus * rating / 100f;

                teamPoints += gain;

                if (!teamsOnly)
                    bonus.Append(Humans.GetFormattedRole(role) + $" ({rating}LV) in " + team.Name, gain);
            }

            if (teamsOnly)
                bonus.Append(team.Name, teamPoints);

            bonus.Append("Maintenance of " + team.Name, Companies.GetManagementCostOfTeam(team));
        }

        return bonus;
    }
}

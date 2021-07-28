using Assets.Core;
using System;

public class ManagerPointsView2 : UpgradedParameterView
{
    int teamId => GetComponent<IsCoreTeam>() == null ? SelectedTeam : 0;
    bool isCoreTeamSpecified => GetComponent<IsCoreTeam>() != null;

    /*Bonus<float> change => isCoreTeamSpecified
        ? Teams.GetManagerPointChange(Flagship, Q)
        : Teams.GetDirectManagementCostOfTeam(Flagship.team.Teams[teamId], Flagship, Q);*/

    TeamInfo team => Flagship.team.Teams[teamId];
    Bonus<float> change => Teams.GetDirectManagementCostOfTeam(team, Flagship, Q);
    
    public override string RenderValue()
    {
        var changeSum = string.Format("{0:+0.00;-0.00}", change.Sum());

        return $"{Visuals.Colorize(changeSum, change.Sum() >= 0)}";
        //return Flagship.companyResource.Resources.managerPoints + $" {Visuals.Colorize(changeSum, change.Sum() >= 0)}";
    }

    public override string RenderHint()
    {
        var sum = change.Sum();

        bool ManagedBadly = team.isManagedBadly;

        var hint = "";

        if (sum >= 0)
        {
            hint = Visuals.Positive("Team is managed properly");
        }
        else
        {
            hint = Visuals.Negative("Team is managed badly");
        }

        return hint + "\n" + change.SortByModule().ToString();
    }
}

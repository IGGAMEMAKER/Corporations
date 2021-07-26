using Assets.Core;
using System;

public class ManagerPointsView2 : UpgradedParameterView
{
    int teamId => GetComponent<IsCoreTeam>() == null ? SelectedTeam : 0;
    bool isCoreTeamSpecified => GetComponent<IsCoreTeam>() != null;

    Bonus<float> change => isCoreTeamSpecified
        ? Teams.GetManagerPointChange(Flagship, Q)
        : Teams.GetDirectManagementCostOfTeam(Flagship.team.Teams[teamId], Flagship, Q);
    
    public override string RenderValue()
    {
        var changeSum = string.Format("{0:+0.00;-0.00}", change.Sum());

        return Flagship.companyResource.Resources.managerPoints + $" {Visuals.Colorize(changeSum, change.Sum() >= 0)}";
    }

    public override string RenderHint()
    {
        return change.SortByModule().ToString();
    }
}

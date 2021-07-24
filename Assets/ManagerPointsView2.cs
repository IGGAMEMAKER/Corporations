using Assets.Core;
using System;

public class ManagerPointsView2 : UpgradedParameterView
{
    int teamId => GetComponent<IsCoreTeam>() == null ? SelectedTeam : 0;
    bool isCoreTeam => GetComponent<IsCoreTeam>() != null;

    Bonus<float> change => Teams.GetManagerPointChange(Flagship, Q);
    
    public override string RenderValue()
    {
        var points = Flagship.companyResource.Resources.managerPoints;

        var changeSum = String.Format("{0:+0.00;-0.00}", change.Sum());

        return points + $" ({Visuals.Colorize(changeSum, change.Sum() >= 0)})";
    }

    public override string RenderHint()
    {
        return change.SortByModule().ToString();
    }
}

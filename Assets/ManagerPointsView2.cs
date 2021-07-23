using Assets.Core;
using System;

public class ManagerPointsView2 : UpgradedParameterView
{
    Bonus<float> change => Teams.GetManagerPointChange(Flagship, Q);
    
    public override string RenderValue()
    {
        var points = Flagship.companyResource.Resources.managerPoints;

        var changeSum = String.Format("{0:+0.00;-0.00}", change.Sum());
        //var changeSum = change.Sum().ToString("+#.00;-#.00;0");
        //var changeSum = change.Sum().ToString("+0.00");

        return points + $" ({Visuals.Colorize(changeSum, change.Sum() >= 0)})";
        //return points + $" ({Visuals.Colorize(changeSum, true)})";
    }

    public override string RenderHint()
    {
        return change.SortByModule().ToString();
    }
}

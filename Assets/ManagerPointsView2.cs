using Assets.Core;

public class ManagerPointsView2 : UpgradedParameterView
{
    public override string RenderValue()
    {
        var points = 100;

        var change = Teams.GetManagerPointChange(Flagship, Q);
        var changeSum = (long) change.Sum();

        return points + $" ({Visuals.Colorize(changeSum, true)})";
    }

    public override string RenderHint()
    {
        var change = Teams.GetManagerPointChange(Flagship, Q);
        
        return change.SortByModule().ToString();
    }
}

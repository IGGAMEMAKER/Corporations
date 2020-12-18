using Assets.Core;

public class ManagerPointsView2 : UpgradedParameterView
{
    Bonus<float> change => Teams.GetManagerPointChange(Flagship, Q);
    public override string RenderValue()
    {
        var points = Flagship.companyResource.Resources.managerPoints;

        var changeSum = (long) change.Sum();

        return points + $" ({Visuals.Colorize(changeSum, true)})";
    }

    public override string RenderHint()
    {
        return change.SortByModule().ToString();
    }
}

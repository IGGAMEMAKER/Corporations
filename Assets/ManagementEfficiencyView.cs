using Assets.Core;

public class ManagementEfficiencyView : UpgradedParameterView
{
    public override string RenderHint()
    {
        var capacity = Companies.GetManagementCapacity(MyCompany, Q);
        var maintenance = Companies.GetManagementCostOfCompany(MyCompany, Q, true);

        var txt = $"Managing our companies costs {maintenance} points (we gain {capacity}).";

        if (maintenance > capacity)
            txt += Visuals.Negative("\n\nDELEGATE to manage bigger companies/teams");

        return txt;
    }

    public override string RenderValue()
    {
        var efficiency = Companies.GetManagementEfficiency(MyCompany, Q);

        Colorize(efficiency, 0, 100);

        return efficiency + "%";
    }
}

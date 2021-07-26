using Assets.Core;

public class ManagementEfficiencyView : UpgradedParameterView
{
    public override string RenderHint()
    {
        var capacity = 0; // Teams.GetManagementCapacity(MyCompany, Q);
        var maintenance = 0; // Teams.GetManagementCostOfCompany(MyCompany, Q, true);

        var txt = $"Managing our companies costs {maintenance} points (we gain {capacity}).";

        if (maintenance > capacity)
            txt += Visuals.Negative("\n\nDELEGATE to manage bigger companies/teams");

        return txt;
    }

    public override string RenderValue()
    {
        var efficiency = 10000; // (int)Teams.GetManagementEfficiency(MyCompany, Q);

        Colorize(efficiency, 0, 100);

        return efficiency + "%";
    }
}

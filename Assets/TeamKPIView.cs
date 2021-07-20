using Assets.Core;
using System.Linq;

public class TeamKPIView : ParameterView
{
    public override string RenderValue()
    {
        string text = "";

        var product = Flagship;

        var team = product.team.Teams[SelectedTeam];



        var devSlots = Teams.GetTeamFeatureSlots(team); // Teams.GetSlotsForTask(team, featureMockup);
        var markSlots = Teams.GetTeamMarketingSlots(team); // Teams.GetSlotsForTask(team, marketingMockup);

        /*text += $"\n\n";

        if (devSlots > 0)
            text += $"{Visuals.Positive("" + devSlots)} features per iteration ";

        if (markSlots > 0)
            text += $"\n{Visuals.Positive("" + markSlots)} ad campaigns ";

        text += "\nslots";*/


        return text;
    }
}

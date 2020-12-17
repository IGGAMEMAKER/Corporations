using Assets;
using Assets.Core;

public class PromoteTeam : ButtonController
{
    public override void Execute()
    {
        var team = Flagship.team.Teams[SelectedTeam];

        Teams.Promote(Flagship, team);
        PlaySound(Sound.GoalCompleted);
    }
}

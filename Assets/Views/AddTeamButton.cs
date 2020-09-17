using Assets.Core;

public class AddTeamButton : ButtonController
{
    public TeamType TeamType;

    public override void Execute()
    {
        Teams.AddTeam(Flagship, TeamType);
    }
}

using Assets.Core;

public class LinkToNextTeam : ButtonController
{
    public int increment = 1;
    public override void Execute()
    {
        var index = SelectedTeam + increment;

        if (index >= Flagship.team.Teams.Count)
        {
            index = 0;
        }

        if (index < 0)
        {
            index = Flagship.team.Teams.Count - 1;
        }

        ScreenUtils.SetSelectedTeam(Q, index);
        Navigate(ScreenMode.TeamScreen);
    }
}

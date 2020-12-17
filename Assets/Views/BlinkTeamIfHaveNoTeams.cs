public class BlinkTeamIfHaveNoTeams : View
{
    Blinker Blinker => Find<Blinker>();
    public override void ViewRender()
    {
        base.ViewRender();

        GetComponent<Blinker>().enabled = Flagship.team.Teams.Count == 0;
    }
}

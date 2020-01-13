public class RenderMoraleProgressbar : View
{
    public override void ViewRender()
    {
        base.ViewRender();

        var morale = SelectedCompany.team.Morale;

        GetComponent<ProgressBar>().SetValue(morale, 100);
    }
}

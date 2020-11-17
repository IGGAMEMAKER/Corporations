using Assets.Core;

public class ToggleCrunchController : ButtonView
{
    public override void Execute()
    {
        Teams.ToggleCrunching(SelectedCompany);

        //ReNavigate();
    }

    public override void ViewRender()
    {
        base.ViewRender();

        ToggleIsChosenComponent(SelectedCompany.isCrunching);
    }
}

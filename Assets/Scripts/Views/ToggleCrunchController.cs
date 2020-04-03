using Assets.Core;

public class ToggleCrunchController : ToggleButtonController
{
    public override void Execute()
    {
        Teams.ToggleCrunching(Q, SelectedCompany.company.Id);

        //ReNavigate();
    }

    public override void ViewRender()
    {
        base.ViewRender();

        ToggleIsChosenComponent(SelectedCompany.isCrunching);
    }
}

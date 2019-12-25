using Assets.Utils;

public class ToggleCrunchController : ToggleButtonController
{
    public override void Execute()
    {
        TeamUtils.ToggleCrunching(GameContext, SelectedCompany.company.Id);

        //ReNavigate();
    }

    public override void ViewRender()
    {
        base.ViewRender();

        ToggleIsChosenComponent(SelectedCompany.isCrunching);
    }
}

using Assets.Core;

public class DumpPrices : ToggleButtonController
{
    public override void Execute()
    {
        Products.ToggleDumping(GameContext, SelectedCompany);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        ToggleIsChosenComponent(SelectedCompany.isDumping);
    }
}

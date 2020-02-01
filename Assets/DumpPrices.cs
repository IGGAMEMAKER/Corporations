using Assets.Core;

public class DumpPrices : ToggleButtonController
{
    public override void Execute()
    {
        Products.ToggleDumping(Q, SelectedCompany);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        ToggleIsChosenComponent(SelectedCompany.isDumping);
    }
}

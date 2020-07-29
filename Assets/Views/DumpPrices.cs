using Assets.Core;

public class DumpPrices : ButtonView
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

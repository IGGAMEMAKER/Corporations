using Assets.Utils;

public class DumpPrices : ToggleButtonController
{
    public override void Execute()
    {
        ProductUtils.ToggleDumping(GameContext, SelectedCompany);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        ToggleIsChosenComponent(SelectedCompany.isDumping);
    }
}

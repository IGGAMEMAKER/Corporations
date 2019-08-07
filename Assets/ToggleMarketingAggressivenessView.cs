public class ToggleMarketingAggressivenessView: View
{
    public override void ViewRender()
    {
        base.ViewRender();

        var c = GetComponent<ToggleMarketingAggressiveness>();

        if (SelectedCompany.hasProduct)
            ToggleIsChosenComponent(SelectedCompany.isAggressiveMarketing);
    }
}
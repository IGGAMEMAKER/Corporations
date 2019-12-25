public class ToggleMarketingAggressiveness : ButtonController
{
    public override void Execute()
    {
        if (SelectedCompany.hasProduct)
            SelectedCompany.isAggressiveMarketing = !SelectedCompany.isAggressiveMarketing;
    }
}

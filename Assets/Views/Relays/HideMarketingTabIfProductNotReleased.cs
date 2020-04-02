public class HideMarketingTabIfProductNotReleased : HideOnSomeCondition
{
    public override bool HideIf() => !SelectedCompany.isRelease;
}

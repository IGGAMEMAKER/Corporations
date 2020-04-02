public class HideReleaseButtonIfReleased : HideOnSomeCondition
{
    public override bool HideIf() => SelectedCompany.isRelease;
}

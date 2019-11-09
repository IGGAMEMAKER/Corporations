public class ToggleProjectButtons : HideOnSomeCondition
{
    public override bool HideIf()
    {
        return !SelectedCompany.hasResearch;
    }
}

using Assets.Core;

public class HideIfHasNoImprovements : HideOnSomeCondition
{
    public override bool HideIf() => !Products.HasFreeImprovements(SelectedCompany);
}

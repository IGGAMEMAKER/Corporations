using Assets.Core;

public class HideIfHasNoImprovements : HideOnSomeCondition
{
    public override bool HideIf() => !Companies.IsRelatedToPlayer(GameContext, SelectedCompany) || !Products.HasFreeImprovements(SelectedCompany);
}

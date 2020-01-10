using Assets.Core;

public class HideIfHasNoImprovements : HideOnSomeCondition
{
    public override bool HideIf()
    {
        return !Companies.IsRelatedToPlayer(GameContext, SelectedCompany)
            || !Products.HasFreeImprovements(SelectedCompany)
            
            // ProductImprovement.Acquisition is not required
            || Cooldowns.IsHasTask(GameContext, new CompanyTaskUpgradeFeature(SelectedCompany.company.Id, ProductImprovement.Acquisition));
    }
}

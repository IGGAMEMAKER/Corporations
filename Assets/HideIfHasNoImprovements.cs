using Assets.Core;

public class HideIfHasNoImprovements : HideOnSomeCondition
{
    public override bool HideIf()
    {
        var p = SelectedCompany;

        return !Companies.IsRelatedToPlayer(GameContext, p)
            || !Products.HasFreeImprovements(p)
            
            // ProductImprovement.Acquisition is not required
            || Cooldowns.IsHasTask(GameContext, new CompanyTaskUpgradeFeature(p.company.Id, ProductFeature.Acquisition))
            
            || Companies.IsReleaseableApp(p, GameContext);
            ;
    }
}

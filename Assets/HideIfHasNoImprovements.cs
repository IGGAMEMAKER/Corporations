using Assets.Core;

public class HideIfHasNoImprovements : HideOnSomeCondition
{
    public override bool HideIf()
    {
        var p = SelectedCompany;

        return !Companies.IsRelatedToPlayer(Q, p)
            || !Products.HasFreeImprovements(p)
            
            // ProductImprovement.Acquisition is not required
            || Cooldowns.IsHasTask(Q, new CompanyTaskUpgradeFeature(p.company.Id, ProductFeature.Acquisition))
            
            || Companies.IsReleaseableApp(p, Q);
            ;
    }
}

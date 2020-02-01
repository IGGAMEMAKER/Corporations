using Assets.Core;

public class HidePrototypeButtonIfWeHaveCompanyOnMarket : HideOnSomeCondition
{
    public override bool HideIf()
    {
        var hasCompanyAlready = Companies.HasCompanyOnMarket(MyCompany, SelectedNiche, Q);
        var isUnknownMarket = !Markets.IsExploredMarket(Q, SelectedNiche);

        return hasCompanyAlready || isUnknownMarket;
    }
}

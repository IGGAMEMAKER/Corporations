using Assets.Utils;

public class HidePrototypeButtonIfWeHaveCompanyOnMarket : HideOnSomeCondition
{
    public override bool HideIf()
    {
        var hasCompanyAlready = Companies.HasCompanyOnMarket(MyCompany, SelectedNiche, GameContext);
        var isUnknownMarket = !NicheUtils.IsExploredMarket(GameContext, SelectedNiche);

        return hasCompanyAlready || isUnknownMarket;
    }
}

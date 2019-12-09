using Assets.Utils;

public class HideMarketResearchButtonIfInProgress : HideOnSomeCondition
{
    public override bool HideIf()
    {
        var niche = NicheUtils.GetNiche(GameContext, SelectedNiche);

        var isResearching = CooldownUtils.IsHasTask(GameContext, new CompanyTaskExploreMarket(SelectedNiche));

        return niche.hasResearch || isResearching;
    }
}

using Assets.Core;

public class HideMarketResearchButtonIfInProgress : HideOnSomeCondition
{
    public override bool HideIf()
    {
        var niche = Markets.GetNiche(GameContext, SelectedNiche);

        var isResearching = Cooldowns.IsHasTask(GameContext, new CompanyTaskExploreMarket(SelectedNiche));

        return niche.hasResearch || isResearching;
    }
}

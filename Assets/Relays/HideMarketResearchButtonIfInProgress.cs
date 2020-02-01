using Assets.Core;

public class HideMarketResearchButtonIfInProgress : HideOnSomeCondition
{
    public override bool HideIf()
    {
        var niche = Markets.GetNiche(Q, SelectedNiche);

        var isResearching = Cooldowns.IsHasTask(Q, new CompanyTaskExploreMarket(SelectedNiche));

        return niche.hasResearch || isResearching;
    }
}

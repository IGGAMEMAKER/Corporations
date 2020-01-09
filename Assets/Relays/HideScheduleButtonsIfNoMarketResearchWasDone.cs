using Assets.Core;

public class HideScheduleButtonsIfNoMarketResearchWasDone : HideOnSomeCondition
{
    public override bool HideIf()
    {
        return CurrentIntDate == 0 && !Cooldowns.IsHasTask(GameContext, new CompanyTaskExploreMarket(SelectedNiche));
    }
}

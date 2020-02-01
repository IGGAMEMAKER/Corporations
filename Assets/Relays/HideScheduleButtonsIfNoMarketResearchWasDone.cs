using Assets.Core;

public class HideScheduleButtonsIfNoMarketResearchWasDone : HideOnSomeCondition
{
    public override bool HideIf()
    {
        return CurrentIntDate == 0 && !Cooldowns.IsHasTask(Q, new CompanyTaskExploreMarket(SelectedNiche));
    }
}

using Assets.Core;

public class HideScheduleButtonsIfNoMarketResearchWasDone : HideOnSomeCondition
{
    public override bool HideIf()
    {
        return CurrentIntDate == 0 && !Cooldowns.HasTask(Q, new CompanyTaskExploreMarket(SelectedNiche));
    }
}

using Assets.Utils;

public class HideScheduleButtonsIfNoMarketResearchWasDone : HideOnSomeCondition
{
    public override bool HideIf()
    {
        return CurrentIntDate == 0 && !CooldownUtils.IsHasTask(GameContext, new CompanyTaskExploreMarket(SelectedNiche));
    }
}

using Assets.Core;

public class HideMarketResearchTask : HideTaskView
{
    public override TaskComponent GetTask()
    {
        return Cooldowns.GetTask(GameContext, new CompanyTaskExploreMarket(SelectedNiche));
    }
}

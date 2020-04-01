using Assets.Core;

public class HideMarketResearchTask : HideTaskView
{
    public override TimedActionComponent GetTask()
    {
        return Cooldowns.GetTask(Q, new CompanyTaskExploreMarket(SelectedNiche));
    }
}

using Assets.Core;

public class HideMarketResearchTask : HideTaskView
{
    public override TaskComponent GetTask()
    {
        return CooldownUtils.GetTask(GameContext, new CompanyTaskExploreMarket(SelectedNiche));
    }
}

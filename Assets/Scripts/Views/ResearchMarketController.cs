using Assets.Core;

public class ResearchMarketController : ButtonController
{
    public override void Execute()
    {
        Cooldowns.AddTask(Q, new CompanyTaskExploreMarket(SelectedNiche), 8);
    }
}

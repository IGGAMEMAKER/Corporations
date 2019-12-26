using Assets.Utils;

public class ResearchMarketController : ButtonController
{
    public override void Execute()
    {
        var res = new Assets.Utils.TeamResource(0, 15, 0, 0, 0);
        //CooldownUtils.AddCooldownAndSpendResources(GameContext, MyCompany, CooldownType.MarketResearch, 15, res);

        CooldownUtils.AddTask(GameContext, new CompanyTaskExploreMarket(SelectedNiche), 8);
        //Navigate(ScreenMode.GroupManagementScreen);
    }
}

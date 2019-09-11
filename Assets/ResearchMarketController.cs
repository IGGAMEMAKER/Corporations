using Assets.Utils;

public class ResearchMarketController : ButtonController
{
    public override void Execute()
    {
        var niche = NicheUtils.GetNicheEntity(GameContext, SelectedNiche);

        CooldownUtils.AddCooldownAndSpendResources(GameContext, MyCompany, CooldownType.MarketResearch, 15, new Assets.Classes.TeamResource(0, 15, 0, 0, 0));

        niche.AddResearch(1);
    }
}

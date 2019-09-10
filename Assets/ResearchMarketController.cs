using Assets.Utils;

public class ResearchMarketController : ButtonController
{
    public override void Execute()
    {
        var niche = NicheUtils.GetNicheEntity(GameContext, SelectedNiche);

        niche.AddResearch(0);
    }
}

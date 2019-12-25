using Assets.Utils;

public class HideMarketInfoIfMarketIsNotResearched : ToggleOnSomeCondition
{
    public override bool Condition()
    {
        var niche = Markets.GetNiche(GameContext, SelectedNiche);

        // or there are no companies

        return niche.hasResearch && Markets.GetCompetitorsAmount(SelectedNiche, GameContext) > 0;
    }
}
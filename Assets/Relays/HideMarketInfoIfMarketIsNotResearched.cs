using Assets.Core;

public class HideMarketInfoIfMarketIsNotResearched : ToggleOnSomeCondition
{
    public override bool Condition()
    {
        var niche = Markets.GetNiche(Q, SelectedNiche);

        // or there are no companies

        return niche.hasResearch && Markets.GetCompetitorsAmount(SelectedNiche, Q) > 0;
    }
}
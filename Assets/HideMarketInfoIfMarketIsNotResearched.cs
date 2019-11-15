using Assets.Utils;

public class HideMarketInfoIfMarketIsNotResearched : ToggleOnSomeCondition
{
    public override bool Condition()
    {
        var niche = NicheUtils.GetNiche(GameContext, SelectedNiche);

        return niche.hasResearch;
    }
}
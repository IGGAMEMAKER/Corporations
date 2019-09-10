using Assets.Utils;

public class HideMarketInfoIfMarketIsNotResearched : ToggleOnSomeCondition
{
    public override bool HideIf()
    {
        var niche = NicheUtils.GetNicheEntity(GameContext, SelectedNiche);

        return niche.hasResearch;
        return true;
    }
}

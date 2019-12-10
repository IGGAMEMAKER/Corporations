using Assets.Utils;

public class HideAcquisitionCandidatesLinkIfNoResearchWasDone : HideOnSomeCondition
{
    public override bool HideIf()
    {
        var niche = Markets.GetNiche(GameContext, SelectedNiche);

        return !niche.hasResearch;
    }
}

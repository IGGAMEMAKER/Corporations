using Assets.Utils;

public class HideAcquisitionCandidatesLinkIfNoResearchWasDone : HideOnSomeCondition
{
    public override bool HideIf()
    {
        var niche = NicheUtils.GetNicheEntity(GameContext, SelectedNiche);

        return !niche.hasResearch;
    }
}

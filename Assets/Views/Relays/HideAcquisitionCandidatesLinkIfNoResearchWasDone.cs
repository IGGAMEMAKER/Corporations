using Assets.Core;

public class HideAcquisitionCandidatesLinkIfNoResearchWasDone : HideOnSomeCondition
{
    public override bool HideIf()
    {
        var niche = Markets.Get(Q, SelectedNiche);

        return !niche.hasResearch;
    }
}

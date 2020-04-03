using Assets.Core;

public class HideAcquisitionCandidatesLinkIfNoResearchWasDone : HideOnSomeCondition
{
    public override bool HideIf()
    {
        var niche = Markets.GetNiche(Q, SelectedNiche);

        return !niche.hasResearch;
    }
}

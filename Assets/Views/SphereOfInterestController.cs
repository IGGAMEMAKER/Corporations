using Assets.Core;

public class SphereOfInterestController : ButtonController
{
    public override void Execute()
    {
        var niche = ScreenUtils.GetSelectedNiche(Q);
        bool isInSphereOfInterest = Companies.IsInSphereOfInterest(MyCompany, niche);

        if (isInSphereOfInterest)
            Companies.RemoveFromSphereOfInfluence(niche, MyCompany, Q);
        else
            Companies.AddFocusNiche(niche, MyCompany, Q);
    }
}

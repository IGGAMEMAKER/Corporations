using Assets.Core;

public class SphereOfInterestController : ButtonController
{
    public override void Execute()
    {
        var niche = SelectedNiche;
        bool isInSphereOfInterest = Companies.IsInSphereOfInterest(MyCompany, niche);

        if (isInSphereOfInterest)
            Companies.RemoveFromSphereOfInfluence(niche, MyCompany, Q);
        else
            Companies.AddFocusNiche(MyCompany, niche, Q);
    }
}

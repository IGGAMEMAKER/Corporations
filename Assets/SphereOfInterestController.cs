using Assets.Core;

public class SphereOfInterestController : ButtonController
{
    public override void Execute()
    {
        var niche = ScreenUtils.GetSelectedNiche(GameContext);
        bool isInSphereOfInterest = Companies.IsInSphereOfInterest(MyCompany, niche);

        if (isInSphereOfInterest)
            Companies.RemoveFromSphereOfInfluence(niche, MyCompany, GameContext);
        else
            Companies.AddFocusNiche(niche, MyCompany, GameContext);
    }
}

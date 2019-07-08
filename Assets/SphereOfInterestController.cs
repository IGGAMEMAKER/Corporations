using Assets.Utils;

public class SphereOfInterestController : ButtonController
{
    public override void Execute()
    {
        var niche = ScreenUtils.GetSelectedNiche(GameContext);
        bool isInSphereOfInterest = CompanyUtils.IsInSphereOfInterest(MyCompany, niche);

        if (isInSphereOfInterest)
            CompanyUtils.RemoveFromSphereOfInfluence(niche, MyCompany, GameContext);
        else
            CompanyUtils.AddFocusNiche(niche, MyCompany, GameContext);
    }
}

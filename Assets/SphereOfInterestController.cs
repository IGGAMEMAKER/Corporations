using Assets.Utils;

public class SphereOfInterestController : ButtonController
{
    public override void Execute()
    {
        var niche = ScreenUtils.GetSelectedNiche(GameContext);
        bool isInSphereOfInterest = CompanyUtils.IsInSphereOfInterest(SelectedCompany, niche);

        GetComponent<IsChosenComponent>().Toggle(isInSphereOfInterest);
    }
}

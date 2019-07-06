using Assets.Utils;
using UnityEngine.UI;

public class SphereOfInterestController : ButtonController
{
    public override void Execute()
    {
        var niche = ScreenUtils.GetSelectedNiche(GameContext);
        bool isInSphereOfInterest = CompanyUtils.IsInSphereOfInterest(SelectedCompany, niche);

        GetComponent<IsChosenComponent>().Toggle(isInSphereOfInterest);

        GetComponentInChildren<Text>().text = isInSphereOfInterest ? "Remove from sphere of interest" : "Add to sphere of interest";

        if (isInSphereOfInterest)
            CompanyUtils.RemoveFromSphereOfInfluence(niche, SelectedCompany, GameContext);
        else
            CompanyUtils.AddFocusNiche(niche, SelectedCompany, GameContext);
    }
}

using Assets.Utils;
using UnityEngine.UI;

public class RenderIsInSphereOfInterest : View
{
    public override void ViewRender()
    {
        base.ViewRender();

        bool isInSphereOfInterest = CompanyUtils.IsInSphereOfInterest(MyCompany, SelectedNiche);

        GetComponent<IsChosenComponent>().Toggle(isInSphereOfInterest);

        GetComponentInChildren<Text>().text = isInSphereOfInterest ? "Remove from sphere of interest" : "Add to sphere of interest";
    }
}

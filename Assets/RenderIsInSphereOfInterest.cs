using Assets.Core;
using TMPro;
using UnityEngine.UI;

public class RenderIsInSphereOfInterest : View
{
    public override void ViewRender()
    {
        base.ViewRender();

        bool isInSphereOfInterest = Companies.IsInSphereOfInterest(MyCompany, SelectedNiche);

        GetComponentInChildren<TextMeshProUGUI>().text = isInSphereOfInterest ? "Remove from sphere of interest" : "Add to sphere of interest";
    }
}

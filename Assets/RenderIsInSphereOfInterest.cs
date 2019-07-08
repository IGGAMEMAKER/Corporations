using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RenderIsInSphereOfInterest : View
{
    public override void ViewRender()
    {
        base.ViewRender();

        var niche = ScreenUtils.GetSelectedNiche(GameContext);
        bool isInSphereOfInterest = CompanyUtils.IsInSphereOfInterest(MyCompany, niche);

        GetComponent<IsChosenComponent>().Toggle(isInSphereOfInterest);

        GetComponentInChildren<Text>().text = isInSphereOfInterest ? "Remove from sphere of interest" : "Add to sphere of interest";
    }
}

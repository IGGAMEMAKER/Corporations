using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpertiseView : View
{
    public LinkToNiche LinkToNiche;
    public ProgressBar ProgressBar;

    NicheType NicheType;

    public override void ViewRender()
    {
        base.ViewRender();

        var exp = SelectedHuman.humanSkills.Expertise[NicheType];

        ProgressBar.SetValue(exp, 100);

        LinkToNiche.SetNiche(NicheType);
    }

    public void SetEntity(NicheType nicheType)
    {
        NicheType = nicheType;
    }
}

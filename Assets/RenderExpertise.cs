using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderExpertise : UpgradedParameterView
{
    public override string RenderHint()
    {
        return "";
    }

    public override string RenderValue()
    {
        return SelectedCompany.expertise.ExpertiseLevel.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesView : ParameterView
{
    public override string RenderValue()
    {
        return Flagship.companyResource.Resources.programmingPoints / C.ITERATION_PROGRESS + "";
    }
}

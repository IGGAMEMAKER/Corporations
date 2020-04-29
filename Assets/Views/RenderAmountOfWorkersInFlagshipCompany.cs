using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderAmountOfWorkersInFlagshipCompany : ParameterView
{
    public override string RenderValue()
    {
        return Teams.GetTeamSize(Flagship, Q).ToString();
    }
}

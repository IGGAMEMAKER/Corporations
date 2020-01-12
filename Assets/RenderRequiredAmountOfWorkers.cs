using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderRequiredAmountOfWorkers : ParameterView
{
    public override string RenderValue()
    {
        return Economy.GetNecessaryAmountOfWorkers(SelectedCompany, GameContext).ToString();
    }
}

using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderRequiredAmountOfWorkers : ParameterView
{
    public override string RenderValue()
    {
        if (SelectedCompany.hasProduct)
            return Products.GetNecessaryAmountOfWorkers(SelectedCompany, Q).ToString();

        return "0";
    }
}

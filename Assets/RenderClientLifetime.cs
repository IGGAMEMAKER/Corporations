using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderClientLifetime : ParameterView
{
    public override string RenderValue()
    {
        var churn = MarketingUtils.GetChurnRate(GameContext, SelectedCompany.company.Id);
        var oppositeChurn = (100 - churn) / 100f;

        var lifetime = Mathf.Log(0.01f, oppositeChurn);

        return lifetime.ToString("0.00") + " months";
    }
}

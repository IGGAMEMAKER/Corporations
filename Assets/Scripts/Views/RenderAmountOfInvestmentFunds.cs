using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RenderAmountOfInvestmentFunds : UpgradedParameterView
{
    public override string RenderHint() => "";

    public override string RenderValue()
    {
        var funds = Companies.GetInvestmentFundsWhoAreInterestedInMarket(Q, SelectedNiche);

        return $"Funds ({funds.Count()})";
    }
}

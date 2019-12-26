using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderCompanyIncome : UpgradedParameterView
{
    public override string RenderHint() => "";

    public override string RenderValue()
    {
        return Format.Money(Economy.GetCompanyIncome(SelectedCompany, GameContext));
    }
}

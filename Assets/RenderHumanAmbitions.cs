using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderHumanAmbitions : UpgradedParameterView
{
    public override string RenderHint()
    {
        return "";
    }

    public override string RenderValue()
    {
        var ambition = HumanUtils.GetFounderAmbition(GameContext, SelectedHuman.human.Id);

        switch (ambition)
        {
            case Ambition.EarnMoney:
                return "Just make money";

            case Ambition.RuleProductCompany:
                return "Make innovative products";

            case Ambition.RuleCorporation:
                return "Rule the corporation";

            default:
                return ambition.ToString();
        }
    }
}

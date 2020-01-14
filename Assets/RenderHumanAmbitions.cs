using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderHumanAmbitions : ParameterView
{
    public override string RenderValue()
    {
        var ambition = Humans.GetFounderAmbition(GameContext, SelectedHuman.human.Id);

        switch (ambition)
        {
            case Ambition.EarnMoney:
                return "Just make money";

            //case Ambition.RuleProductCompany:
            //    return "Make innovative products";

            case Ambition.RuleCorporation:
                return "Rule the corporation";

            default:
                return ambition.ToString();
        }
    }
}

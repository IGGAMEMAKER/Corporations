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
            case Ambition.CreateUnicorn:
                return "Create Unicorn ($1B company)";

            case Ambition.EarnMoney:
                return "Just make money";

            case Ambition.IPO:
                return "Create company and make it public!";

            case Ambition.RuleCorporation:
                return "Rule the corporation";

            case Ambition.RuleProductCompany:
                return "Make innovative products";

            default:
                return ambition.ToString();
        }
    }
}

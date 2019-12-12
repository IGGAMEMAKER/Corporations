using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderInnovationChanceDescription : UpgradedParameterView
{
    public override string RenderHint()
    {
        return "";
    }

    public override string RenderValue()
    {
        var chance = Products.GetInnovationChance(SelectedCompany, GameContext);
        var text = $"<b>Innovation chance: {chance}%</b>\n\n";

        if (Companies.IsExploredCompany(GameContext, SelectedCompany))
        {
            var description = Products.GetInnovationChanceBonus(SelectedCompany, GameContext).ToString();

            return text + description;
        }

        return text;
    }
}

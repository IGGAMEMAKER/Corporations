using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderInnovationChanceDescription : ParameterView
{
    public override string RenderValue()
    {
        if (!SelectedCompany.hasProduct)
            return "";

        var chance = Products.GetInnovationChance(SelectedCompany, GameContext);
        var text = $"<b>Innovation chance\n{chance}%</b>\n\n";

        if (Companies.IsExploredCompany(GameContext, SelectedCompany))
        {
            var description = Products.GetInnovationChanceBonus(SelectedCompany, GameContext).ToString();

            return text + description;
        }

        return text;
    }
}

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
        if (CompanyUtils.IsExploredCompany(GameContext, SelectedCompany))
        {
            var expertise = ProductUtils.GetInnovationChance(SelectedCompany, GameContext);
            var text = $"Innovation chance: {expertise}%\n\n";

            var description = ProductUtils.GetInnovationChanceDescription(SelectedCompany, GameContext).ToString();

            return text + description;
        }

        return "Innovation chance: ??? \n\nResearch the company to find out!";
    }
}

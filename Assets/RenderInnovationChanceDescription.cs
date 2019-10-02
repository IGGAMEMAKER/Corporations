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
        if (SelectedCompany.hasResearch || CompanyUtils.IsCompanyRelatedToPlayer(GameContext, SelectedCompany))
            return ProductUtils.GetInnovationChanceDescription(SelectedCompany, GameContext).ToString();

        return "??? Make company research to find out!";
    }
}

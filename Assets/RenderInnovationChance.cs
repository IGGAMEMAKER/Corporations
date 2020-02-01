using Assets.Core;
using System;

public class RenderInnovationChance : UpgradedParameterView
{
    public override string RenderHint()
    {
        if (!SelectedCompany.hasProduct)
            return "";

        if (Companies.IsExploredCompany(GameContext, SelectedCompany))
            return Products.GetInnovationChanceBonus(SelectedCompany, GameContext).ToString();

        return "Research company to get more details";
    }

    public override string RenderValue()
    {
        if (!SelectedCompany.hasProduct)
            return "";

        var chance = Products.GetInnovationChance(SelectedCompany, GameContext);

        return chance + "%";
    }
}

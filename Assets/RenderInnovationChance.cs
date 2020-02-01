using Assets.Core;
using System;

public class RenderInnovationChance : UpgradedParameterView
{
    public override string RenderHint()
    {
        if (!SelectedCompany.hasProduct)
            return "";

        if (Companies.IsExploredCompany(Q, SelectedCompany))
            return Products.GetInnovationChanceBonus(SelectedCompany, Q).ToString();

        return "Research company to get more details";
    }

    public override string RenderValue()
    {
        if (!SelectedCompany.hasProduct)
            return "";

        var chance = Products.GetInnovationChance(SelectedCompany, Q);

        return chance + "%";
    }
}

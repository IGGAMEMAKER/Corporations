using Assets.Utils;
using System;

public class RenderInnovationChance : UpgradedParameterView
{
    public override string RenderHint()
    {
        return Products.GetInnovationChanceBonus(SelectedCompany, GameContext).ToString();
    }

    public override string RenderValue()
    {
        var chance = Products.GetInnovationChance(SelectedCompany, GameContext);

        Colorize(chance, 0, 70);

        return chance + "%";
    }
}

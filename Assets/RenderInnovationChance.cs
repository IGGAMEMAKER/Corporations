using Assets.Utils;
using System;

public class RenderInnovationChance : UpgradedParameterView
{
    public override string RenderHint()
    {
        return ProductUtils.GetInnovationChanceDescription(SelectedCompany, GameContext).ToString();
    }

    public override string RenderValue()
    {
        var chance = ProductUtils.GetInnovationChance(SelectedCompany, GameContext);

        Colorize(chance, 0, 70);

        return chance + "%";
    }
}

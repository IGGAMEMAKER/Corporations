using Assets.Utils;
using System;

public class RenderInnovationChance : ParameterView
{
    public override string RenderValue()
    {
        var chance = Products.GetInnovationChance(SelectedCompany, GameContext);

        Colorize(chance, 0, 70);

        return chance + "%";
    }
}

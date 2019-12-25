using Assets.Utils;
using System;

public class RenderInnovationChance : ParameterView
{
    public override string RenderValue()
    {
        if (!SelectedCompany.hasProduct)
            return "";

        var chance = Products.GetInnovationChance(SelectedCompany, GameContext);

        return chance + "%";
    }
}

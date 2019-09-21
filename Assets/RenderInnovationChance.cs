using Assets.Utils;
using System;

public class RenderInnovationChance : UpgradedParameterView
{
    public override string RenderHint()
    {
        return CompanyUtils.GetInnovationChanceDescription(SelectedCompany, GameContext).ToString();
    }

    public override string RenderValue()
    {
        var chance = CompanyUtils.GetInnovationChance(SelectedCompany, GameContext);

        Colorize(chance, 0, 70);

        return chance + "%";
    }
}

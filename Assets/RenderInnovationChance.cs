using Assets.Utils;
using System;

public class RenderInnovationChance : UpgradedParameterView
{
    public override string RenderHint()
    {
        return CompanyUtils.GetInnovationChanceDescription(SelectedCompany).ToString();
    }

    public override string RenderValue()
    {
        var chance = CompanyUtils.GetInnovationChance(SelectedCompany);

        Colorize(chance, 0, 70);

        return chance + "%";
    }
}

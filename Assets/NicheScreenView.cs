using Assets.Core;
using Assets.Core.Formatting;
using UnityEngine.UI;

public class NicheScreenView : UpgradedParameterView
{
    public override string RenderValue()
    {
        NicheType NicheType = ScreenUtils.GetSelectedNiche(Q);
        IndustryType IndustryType = Markets.GetIndustry(NicheType, Q);

        return Visuals.Link("Is part of " + EnumUtils.GetFormattedIndustryName(IndustryType) + " industry");
    }

    public override string RenderHint()
    {
        return "";
    }
}

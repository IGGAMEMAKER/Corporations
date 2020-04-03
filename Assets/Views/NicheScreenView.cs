using Assets.Core;
using UnityEngine.UI;

public class NicheScreenView : UpgradedParameterView
{
    public override string RenderValue()
    {
        NicheType NicheType = ScreenUtils.GetSelectedNiche(Q);
        IndustryType IndustryType = Markets.GetIndustry(NicheType, Q);

        return Visuals.Link("Is part of " + Enums.GetFormattedIndustryName(IndustryType) + " industry");
    }

    public override string RenderHint()
    {
        return "";
    }
}

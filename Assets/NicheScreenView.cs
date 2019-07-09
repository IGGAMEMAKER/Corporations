using Assets.Utils;
using Assets.Utils.Formatting;
using UnityEngine.UI;

public class NicheScreenView : UpgradedParameterView
{
    public override string RenderValue()
    {
        NicheType NicheType = ScreenUtils.GetSelectedNiche(GameContext);
        IndustryType IndustryType = NicheUtils.GetIndustry(NicheType, GameContext);

        return Visuals.Link("Is part of " + EnumUtils.GetFormattedIndustryName(IndustryType) + " industry");
    }

    public override string RenderHint()
    {
        return "";
    }
}

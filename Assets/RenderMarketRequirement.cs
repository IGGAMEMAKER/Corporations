using Assets.Utils;
using UnityEngine.UI;

public class RenderMarketRequirement : UpgradedParameterView
{
    public override string RenderHint()
    {
        return "";
    }

    public override string RenderValue()
    {
        return ProductUtils.GetMarketDemand(SelectedCompany, GameContext).ToString();
    }
}

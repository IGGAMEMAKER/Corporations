using Assets.Utils;
using UnityEngine.UI;

public class RenderMarketRequirement : ParameterView
{
    public override string RenderValue()
    {
        if (!SelectedCompany.hasProduct)
            return "";

        return Products.GetMarketRequirements(SelectedCompany, GameContext) + "LVL";
    }
}

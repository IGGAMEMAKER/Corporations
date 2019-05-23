using Assets.Utils;
using UnityEngine.UI;

public class RenderMarketRequirement : View
{
    public override void ViewRender()
    {
        base.ViewRender();

        GetComponent<Text>().text = NicheUtils.GetMarketDemand(GameContext, ScreenUtils.GetSelectedNiche(GameContext)) + "LVL";
    }
}

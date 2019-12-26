using Assets.Core;
using UnityEngine.UI;

public class RenderLinkToMarket : View
{
    public override void ViewRender()
    {
        base.ViewRender();

        bool show = Markets.IsExploredMarket(GameContext, SelectedNiche) && MyCompany.isWantsToExpand;

        GetComponent<LinkTo>().enabled = show;
        GetComponent<Text>().text = show ? Visuals.Link("Market Info") : "";
    }
}

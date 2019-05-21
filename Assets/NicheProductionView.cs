using Assets.Utils;
using UnityEngine.UI;

public class NicheProductionView : View
{
    public Text IdeasCost;
    public Text TechCost;

    public override void ViewRender()
    {
        base.ViewRender();

        var niche = ScreenUtils.GetSelectedNiche(GameContext);

        var costs = NicheUtils.GetNicheEntity(GameContext, niche).nicheCosts;

        IdeasCost.text = costs.IdeaCost.ToString();
        TechCost.text = costs.TechCost.ToString();
    }
}

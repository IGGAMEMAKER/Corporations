using Assets.Utils;
using UnityEngine.UI;

public class NicheProductionView : View
{
    public Text IdeasCost;
    public Text TechCost;
    public Text SalesCost;
    public Text AdCost;

    public override void ViewRender()
    {
        base.ViewRender();

        var niche = ScreenUtils.GetSelectedNiche(GameContext);

        var costs = NicheUtils.GetNicheEntity(GameContext, niche).nicheCosts;

        IdeasCost.text = Format.Shorten(costs.IdeaCost);
        TechCost.text = Format.Shorten(costs.TechCost);
        SalesCost.text = Format.Shorten(costs.MarketingCost);

        AdCost.text = Format.Shorten(costs.AdCost);
    }
}

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

        IdeasCost.text = Format.Minify(costs.IdeaCost);
        TechCost.text = Format.Minify(costs.TechCost);
        SalesCost.text = Format.Minify(costs.MarketingCost);

        AdCost.text = Format.Minify(costs.AdCost);
    }
}

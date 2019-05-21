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

        IdeasCost.text = ValueFormatter.Shorten(costs.IdeaCost);
        TechCost.text = ValueFormatter.Shorten(costs.TechCost);
        SalesCost.text = ValueFormatter.Shorten(costs.MarketingCost);

        AdCost.text = ValueFormatter.Shorten(costs.AdCost);
    }
}

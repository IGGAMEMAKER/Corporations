using Assets.Core;
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

        var costs = Markets.GetNicheCosts(GameContext, niche);

        IdeasCost.text = "???";
        TechCost.text = Format.Minify(costs.TechCost);
        SalesCost.text = "????";

        AdCost.text = Format.Minify(costs.AcquisitionCost);
    }
}

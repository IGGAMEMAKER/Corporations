using Assets.Core;
using UnityEngine;

public class DevelopmentScreenView : View
{
    public GameObject Managers;

    public GameObject ProductProfitLabel;
    public GameObject ProductProfit;

    public GameObject Goal;

    public GameObject MarketRequirements;
    public GameObject MarketRequirementsLabel;

    public GameObject Upgrades;

    public override void ViewRender()
    {
        base.ViewRender();

        var hasReleasedProducts = Companies.IsHasReleasedProducts(Q, MyCompany);

        var beforeRelease = !hasReleasedProducts;

        // after release
        Managers       .SetActive(hasReleasedProducts);

        ProductProfit       .SetActive(hasReleasedProducts);
        ProductProfitLabel  .SetActive(hasReleasedProducts);

        Goal    .SetActive(hasReleasedProducts);

        // pre release
        MarketRequirements  .SetActive(beforeRelease);
        MarketRequirementsLabel.SetActive(beforeRelease);
        Upgrades            .SetActive(beforeRelease);
    }
}

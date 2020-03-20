using Assets.Core;
using UnityEngine;

public class DevelopmentScreenView : View
{
    public GameObject ManagersLabel;
    public GameObject ManagersList;

    public GameObject ProductProfitLabel;
    public GameObject ProductProfit;

    public GameObject CompetitionLabel;
    public GameObject LinkToMarketLabel;

    public GameObject MarketRequirements;

    public GameObject Upgrades;

    public override void ViewRender()
    {
        base.ViewRender();

        var hasReleasedProducts = Companies.IsHasReleasedProducts(Q, MyCompany);

        var beforeRelease = !hasReleasedProducts;

        // after release
        ManagersLabel       .SetActive(hasReleasedProducts);
        ManagersList        .SetActive(hasReleasedProducts);

        ProductProfit       .SetActive(hasReleasedProducts);
        ProductProfitLabel  .SetActive(hasReleasedProducts);

        CompetitionLabel    .SetActive(hasReleasedProducts);
        LinkToMarketLabel   .SetActive(hasReleasedProducts);

        // pre release
        MarketRequirements  .SetActive(beforeRelease);
        Upgrades            .SetActive(beforeRelease);
    }
}

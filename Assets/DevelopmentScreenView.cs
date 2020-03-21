using Assets.Core;
using UnityEngine;

public class DevelopmentScreenView : View
{
    public GameObject ManagersLabel;
    public GameObject ManagersList;
    public GameObject HireManagersLabel;
    public GameObject HireManagersList;

    public GameObject ProductProfitLabel;
    public GameObject ProductProfit;

    public GameObject CompetitionLabel;
    public GameObject LinkToMarketLabel;

    public GameObject MarketRequirements;
    public GameObject MarketRequirementsLabel;

    public GameObject Upgrades;

    public override void ViewRender()
    {
        base.ViewRender();

        var hasReleasedProducts = Companies.IsHasReleasedProducts(Q, MyCompany);

        var beforeRelease = !hasReleasedProducts;

        // after release
        ManagersLabel       .SetActive(hasReleasedProducts);
        ManagersList        .SetActive(hasReleasedProducts);
        HireManagersLabel       .SetActive(hasReleasedProducts);
        HireManagersList        .SetActive(hasReleasedProducts);

        ProductProfit       .SetActive(hasReleasedProducts);
        ProductProfitLabel  .SetActive(hasReleasedProducts);

        CompetitionLabel    .SetActive(hasReleasedProducts);
        LinkToMarketLabel   .SetActive(hasReleasedProducts);

        // pre release
        MarketRequirements  .SetActive(beforeRelease);
        MarketRequirementsLabel.SetActive(beforeRelease);
        Upgrades            .SetActive(beforeRelease);
    }
}

using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RenderProductStatsInCompanyView : View
{
    public Text Clients;

    public GameObject MarketShare;
    public GameObject MarketShareLabel;

    public Text ProductLevel;
    public GameObject ProductLevelLabel;

    public Text Brand;
    public GameObject BrandIcon;

    public void Render(GameEntity company)
    {
        Clients.text = Format.Minify(Marketing.GetClients(company));

        // market share
        bool isPlayerFlagship = company.company.Id == Flagship.company.Id;
        bool needToShowMarketShare = company.isRelease;

        Draw(MarketShare, needToShowMarketShare);
        Draw(MarketShareLabel, false);

        // product level
        var levelStatus = Products.GetConceptStatus(company, Q);
        var statusColor = Colors.COLOR_WHITE;

        if (levelStatus == ConceptStatus.Leader)
            statusColor = Colors.COLOR_BEST;

        if (levelStatus == ConceptStatus.Outdated)
            statusColor = Colors.COLOR_NEGATIVE;

        var market = Markets.Get(Q, company);
        var maxLevel = Products.GetMarketDemand(market);

        var outOf = "";
        if (!company.isRelease)
        {
            outOf = $" / {maxLevel}";
        }

        var level = Products.GetProductLevel(company);
        ProductLevel.text = level + outOf + "LVL";
        ProductLevel.color = Visuals.GetGradientColor(0, maxLevel, level);

        // brand
        Brand.text = (int)company.branding.BrandPower + "";
        Draw(BrandIcon, false); // company.isRelease
        Draw(Brand, false);

        // workers
    }
}

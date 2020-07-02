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

    public Text Growth;

    public Text Teams;

    public void Render(GameEntity company)
    {
        Clients.text = Format.Minify(Marketing.GetClients(company));

        // market share
        bool isPlayerFlagship = company.company.Id == Flagship.company.Id;
        bool needToShowMarketShare = company.isRelease;

        var share = Companies.GetMarketShareOfCompanyMultipliedByHundred(company, Q);
        MarketShare.GetComponent<Text>().text = share.ToString("0") + "%";
        MarketShare.GetComponent<Text>().color = Visuals.GetGradientColor(0, 100, share);

        Draw(MarketShare, true);
        Draw(MarketShareLabel, false);

        ResizeFirmLogo(company);

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

        var str = "";
        foreach (var t in company.team.Teams)
        {
            str += "* " + t.Name + "\n";
            for (var taskId = 0; taskId < t.Tasks.Count; taskId++)
            {
                var task = t.Tasks[taskId];
                str += "  " + task.GetTaskName() + "\n";
            }
        }
        Teams.text = str;

        var growth = Marketing.GetAudienceGrowth(company, Q);

        Growth.text = Format.SignOf(growth) + Format.Minify(growth) + " weekly";
        Growth.color = Visuals.GetColorPositiveOrNegative(growth);
    }

    void ResizeFirmLogo(GameEntity company)
    {
        var scale = 1f;

        //var company = Flagship;

        var marketShare = Companies.GetMarketShareOfCompanyMultipliedByHundred(company, Q);

        // share = 0
        var minSize = 0.85f;

        // share = 100
        var maxSize = 2.5f;

        scale = minSize + (maxSize - minSize) * marketShare / 100;

        transform.localScale = new Vector3(scale, scale, scale);
    }
}

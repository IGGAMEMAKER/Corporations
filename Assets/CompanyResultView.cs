﻿using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompanyResultView : View
{
    public LinkToNiche LinkToNiche;
    public Text CompanyName;
    public Text ClientGrowth;
    public Text MarketShareChange;
    public Text ConceptStatusText;
    public Text Profit;

    public ToggleMarketingFinancing ToggleMarketingFinancing;

    public void SetEntity(ProductCompanyResult result)
    {
        CompanyName.text = CompanyUtils.GetCompanyById(GameContext, result.CompanyId).company.Name;

        var growth = result.clientChange;
        var product = CompanyUtils.GetCompanyById(GameContext, result.CompanyId);
        growth = MarketingUtils.GetAudienceGrowthMultiplier(product, GameContext);

        var growthMultiplier = MarketingUtils.GetGrowthMultiplier(product, GameContext);
        ClientGrowth.text = "Client growth\n" + Visuals.PositiveOrNegativeMinified(growth) + "%";
        ClientGrowth.gameObject.GetComponent<Hint>().SetHint(growthMultiplier.ToString());

        var share = (long)result.MarketShareChange;
        MarketShareChange.text = "Market share\n" + Visuals.PositiveOrNegativeMinified(share) + "%";



        var conceptStatus = result.ConceptStatus;
        var color = GetStatusColor(conceptStatus);

        ConceptStatusText.text = "Product\n" + Visuals.Colorize(conceptStatus.ToString(), color);

        var c = CompanyUtils.GetCompanyById(GameContext, result.CompanyId);
        LinkToNiche.SetNiche(c.product.Niche);

        var profit = EconomyUtils.GetProfit(GameContext, result.CompanyId);
        Profit.text = "Profit\n" + Visuals.Colorize(Format.Money(profit), profit > 0);

        ToggleMarketingFinancing.SetCompanyId(result.CompanyId);
    }

    string GetStatusColor (ConceptStatus conceptStatus)
    {
        if (conceptStatus == ConceptStatus.Leader)
            return VisualConstants.COLOR_POSITIVE;

        if (conceptStatus == ConceptStatus.Outdated)
            return VisualConstants.COLOR_NEGATIVE;

        return VisualConstants.COLOR_NEUTRAL;
    }
}

using Assets.Utils;
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

    public void SetEntity(ProductCompanyResult result)
    {
        CompanyName.text = CompanyUtils.GetCompanyById(GameContext, result.CompanyId).company.Name;

        var growth = result.clientChange;
        ClientGrowth.text = "Client growth\n" + Visuals.PositiveOrNegativeMinified(growth);

        var share = (long)result.MarketShareChange;
        MarketShareChange.text = "Market share\n" + Visuals.PositiveOrNegativeMinified(share) + "%";



        var conceptStatus = result.ConceptStatus;
        var color = GetStatusColor(conceptStatus);

        ConceptStatusText.text = "Product\n" + Visuals.Colorize(conceptStatus.ToString(), color);

        var c = CompanyUtils.GetCompanyById(GameContext, result.CompanyId);
        LinkToNiche.SetNiche(c.product.Niche);
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

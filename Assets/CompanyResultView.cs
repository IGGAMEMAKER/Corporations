using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompanyResultView : View
{
    public LinkToProjectView LinkToProjectView;
    public Text CompanyName;
    public Text ClientGrowth;
    public Text MarketShareChange;
    public Text ConceptStatusText;

    public void SetEntity(ProductCompanyResult result)
    {
        CompanyName.text = CompanyUtils.GetCompanyById(GameContext, result.CompanyId).company.Name;

        //ClientGrowth.color = Visuals.G
        ClientGrowth.text = "Client growth\n" + Format.Minify(result.clientChange);

        MarketShareChange.text = "Market share\n" + Format.Sign((long)result.MarketShareChange) + "%";

        ConceptStatusText.text = "Product\n" + result.ConceptStatus;

        LinkToProjectView.CompanyId = result.CompanyId;
    }
}

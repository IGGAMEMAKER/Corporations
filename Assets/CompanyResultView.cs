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
        ClientGrowth.text = Format.Minify(result.clientChange);

        MarketShareChange.text = result.MarketShareChange.ToString();

        ConceptStatusText.text = result.ConceptStatus.ToString();

        LinkToProjectView.CompanyId = result.CompanyId;
    }
}

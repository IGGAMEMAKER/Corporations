using Assets.Utils;
using UnityEngine;
using UnityEngine.UI;

public class RisksView : View
    , IAnyDateListener
{
    public GameObject RiskContainer;

    public GameObject TotalRisk;
    public GameObject NicheDemandRisk;
    public GameObject MonetisationRisk;
    public GameObject CompetitorsRisk;

    void OnEnable()
    {
        Render();

        LazyUpdate(this);
    }

    void Render()
    {
        var c = SelectedCompany;

        if (CompanyUtils.IsCompanyGroupLike(c))
        {
            RiskContainer.SetActive(false);
            return;
        }

        RiskContainer.SetActive(true);

        var companyId = c.company.Id;

        int risk = NicheUtils.GetCompanyRisk(GameContext, companyId);

        TotalRisk.GetComponent<ColoredValueGradient>().value = risk;
        TotalRisk.GetComponent<Hint>().SetHint($"This reduces base cost by {risk}%");

        NicheDemandRisk.GetComponent<Text>().text = RenderRisk(NicheUtils.GetMarketDemandRisk(GameContext, companyId));
        MonetisationRisk.GetComponent<Text>().text = RenderRisk(NicheUtils.GetMonetisationRisk(GameContext, companyId));
        CompetitorsRisk.GetComponent<Text>().text = RenderRisk(NicheUtils.GetCompetititiveRiskOnNiche(GameContext, companyId));
    }

    string RenderRisk(int modifier)
    {
        return modifier + "%";
    }

    void IAnyDateListener.OnAnyDate(GameEntity entity, int date)
    {
        Render();
    }
}

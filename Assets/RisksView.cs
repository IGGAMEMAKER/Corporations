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

        var text = $"This reduces base company cost by {risk}%\n {NicheUtils.GetCompanyRiskDescription(GameContext, companyId)}";

        TotalRisk.GetComponent<Hint>().SetHint(text);
    }

    void IAnyDateListener.OnAnyDate(GameEntity entity, int date)
    {
        Render();
    }
}

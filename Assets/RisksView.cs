using Assets.Utils;
using UnityEngine;

public class RisksView : View
{
    public GameObject RiskContainer;

    public GameObject TotalRisk;
    public GameObject NicheDemandRisk;
    public GameObject MonetisationRisk;
    public GameObject CompetitorsRisk;

    public override void ViewRender()
    {
        base.ViewRender();

        var c = SelectedCompany;

        if (CompanyUtils.IsCompanyGroupLike(c))
        {
            RiskContainer.SetActive(false);

            return;
        }

        RiskContainer.SetActive(true);

        var companyId = c.company.Id;

        var risk = NicheUtils.GetCompanyRisk(GameContext, companyId);

        var text = $"This reduces base company cost by {risk}%\n {NicheUtils.GetCompanyRiskDescription(GameContext, companyId)}";

        TotalRisk.GetComponent<ColoredValueGradient>().value = risk;
        TotalRisk.GetComponent<Hint>().SetHint(text);
    }
}

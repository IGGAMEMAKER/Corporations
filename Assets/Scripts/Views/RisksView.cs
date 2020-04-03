using Assets.Core;
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

        if (Companies.IsCompanyGroupLike(c))
        {
            RiskContainer.SetActive(false);

            return;
        }

        RiskContainer.SetActive(true);

        var companyId = c.company.Id;

        var risk = Markets.GetCompanyRisk(Q, companyId);

        var text = $"This reduces base company cost by {risk}%\n {Markets.GetCompanyRiskDescription(Q, companyId)}";

        TotalRisk.GetComponent<ColoredValueGradient>().UpdateValue(risk);
        TotalRisk.GetComponent<Hint>().SetHint(text);
    }
}

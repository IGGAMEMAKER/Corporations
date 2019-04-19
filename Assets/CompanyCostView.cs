using Assets.Utils;
using UnityEngine.UI;

public class CompanyCostView : View
{
    public Text CompanyCost;
    public Hint CompanyCostHint;

    public Text BaseCost;

    public Text AudienceCost;
    public Text AudienceLabel;

    public Text IncomeBasedCost;
    public Text IncomeBasedCostLabel;
    public Text CapitalSize;

    public Text HoldingCost;
    public Text HoldingLabel;


    void OnEnable()
    {
        Render();
    }

    string RenderCosts(long cost)
    {
        return "$" + ValueFormatter.Shorten(cost);
    }

    void ShowGroupLabels(bool show)
    {
        HoldingCost.gameObject.SetActive(show);
        HoldingLabel.gameObject.SetActive(show);
    }

    void ShowProductCompanyLabels(bool show)
    {
        IncomeBasedCost.gameObject.SetActive(show);
        IncomeBasedCostLabel.gameObject.SetActive(show);

        AudienceCost.gameObject.SetActive(show);
        AudienceLabel.gameObject.SetActive(show);
    }

    void RenderBaseCosts(int companyId, GameEntity c)
    {
        BaseCost.text = RenderCosts(CompanyEconomyUtils.GetCompanyBaseCost(GameContext, companyId));
        CapitalSize.text = RenderCosts(c.companyResource.Resources.money);

        if (CompanyUtils.IsProductCompany(c))
        {
            ShowProductCompanyLabels(true);
            ShowGroupLabels(false);

            AudienceCost.text = RenderCosts(CompanyEconomyUtils.GetClientBaseCost(GameContext, companyId));
            IncomeBasedCost.text = RenderCosts(CompanyEconomyUtils.GetCompanyIncomeBasedCost(GameContext, companyId));
            IncomeBasedCostLabel.text = $"Income X{CompanyEconomyUtils.GetCompanyCostNicheMultiplier()}";
        }
        else
        {
            ShowProductCompanyLabels(false);
            ShowGroupLabels(true);

            HoldingCost.text = RenderCosts(CompanyEconomyUtils.GetHoldingCost(GameContext, companyId));
        }
    }

    void Render()
    {
        var c = SelectedCompany;
        var companyId = c.company.Id;

        CompanyCost.text = RenderCosts(CompanyEconomyUtils.GetCompanyCost(GameContext, companyId));

        RenderBaseCosts(companyId, c);

    }
}

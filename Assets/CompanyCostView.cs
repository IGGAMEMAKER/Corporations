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


    string RenderCosts(long cost)
    {
        return "$" + Format.Minify(cost);
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

        AudienceCost.gameObject.SetActive(show && false);
        AudienceLabel.gameObject.SetActive(show);
    }

    void RenderBaseCosts(int companyId, GameEntity c)
    {
        BaseCost.text = RenderCosts(EconomyUtils.GetCompanyBaseCost(GameContext, companyId));
        CapitalSize.text = RenderCosts(c.companyResource.Resources.money);

        if (CompanyUtils.IsProductCompany(c))
        {
            ShowProductCompanyLabels(true);
            ShowGroupLabels(false);

            AudienceCost.text = RenderCosts(EconomyUtils.GetClientBaseCost(GameContext, companyId));
            IncomeBasedCost.text = RenderCosts(EconomyUtils.GetCompanyIncomeBasedCost(GameContext, companyId));
            IncomeBasedCostLabel.text = $"Income X{EconomyUtils.GetCompanyCostNicheMultiplier()}";
        }
        else
        {
            ShowProductCompanyLabels(false);
            ShowGroupLabels(true);

            HoldingCost.text = RenderCosts(EconomyUtils.GetHoldingCost(GameContext, companyId));
        }
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var c = SelectedCompany;
        var companyId = c.company.Id;

        CompanyCost.text = RenderCosts(EconomyUtils.GetCompanyCost(GameContext, companyId));

        RenderBaseCosts(companyId, c);
    }
}

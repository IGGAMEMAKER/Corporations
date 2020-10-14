using Assets.Core;
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

    void RenderBaseCosts(GameEntity c)
    {
        BaseCost.text = "???!!!"; // RenderCosts(Economy.GetCompanyBaseCost(c, Q));
        CapitalSize.text = RenderCosts(Economy.BalanceOf(c));

        if (Companies.IsProduct(c))
        {
            ShowProductCompanyLabels(true);
            ShowGroupLabels(false);

            AudienceCost.text = RenderCosts(Economy.GetClientBaseCost(c));
            IncomeBasedCost.text = "???!!!"; // RenderCosts(Economy.GetCompanyIncomeBasedCost(c, Q));
            IncomeBasedCostLabel.text = $"Income X{Economy.GetCompanyCostNicheMultiplier()}";
        }
        else
        {
            ShowProductCompanyLabels(false);
            ShowGroupLabels(true);

            HoldingCost.text = RenderCosts(Economy.GetGroupCost(c, Q));
        }
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var c = SelectedCompany;

        CompanyCost.text = RenderCosts(Economy.CostOf(c, Q));

        RenderBaseCosts(c);
    }
}

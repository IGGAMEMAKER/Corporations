using Assets.Utils;
using UnityEngine.UI;

public class CompanyCostView : View
{
    public Text CompanyCost;
    public Hint CompanyCostHint;

    public Text BaseCost;
    public Text AudienceCost;
    public Text IncomeBasedCost;
    public Text IncomeBasedCostLabel;

    // Start is called before the first frame update
    void OnEnable()
    {
        Render();
    }

    string RenderCosts(long cost)
    {
        return "$" + ValueFormatter.Shorten(cost);
    }

    void RenderBaseCosts(int companyId, GameEntity c)
    {
        BaseCost.text = RenderCosts(CompanyEconomyUtils.GetCompanyBaseCost(GameContext, companyId));

        if (CompanyUtils.IsProductCompany(c))
        {
            AudienceCost.gameObject.SetActive(true);
            AudienceCost.text = RenderCosts(CompanyEconomyUtils.GetClientBaseCost(GameContext, companyId));
        }
        else
        {
            AudienceCost.gameObject.SetActive(false);
        }

        IncomeBasedCost.text = RenderCosts(CompanyEconomyUtils.GetCompanyIncomeBasedCost(GameContext, companyId));
        IncomeBasedCostLabel.text = $"Income X{CompanyEconomyUtils.GetCompanyCostNicheMultiplier()}";
    }

    void Render()
    {
        var c = SelectedCompany;
        var companyId = c.company.Id;

        CompanyCost.text = RenderCosts(CompanyEconomyUtils.GetCompanyCost(GameContext, companyId));

        RenderBaseCosts(companyId, c);

    }
}

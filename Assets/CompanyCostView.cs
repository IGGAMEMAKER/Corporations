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

    public Text TotalCostModifiers;
    public Text MarketGrowthModifier;
    public Text DividendsModifier;
    public Text Risks;
    public Hint RisksHint;
    public Text ProductPotential;

    // Start is called before the first frame update
    void OnEnable()
    {
        Render();
    }

    string RenderCosts(long cost)
    {
        return "$" + ValueFormatter.Shorten(cost);
    }

    string RenderModifier(int modifier)
    {
        return modifier.ToString();
    }

    void RenderBaseCosts(int companyId, GameEntity c)
    {
        BaseCost.text = RenderCosts(CompanyEconomyUtils.GetCompanyCost(GameContext, companyId));

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
        IncomeBasedCostLabel.text = $"Income X{CompanyEconomyUtils.GetCompanyCostEnthusiasm()}";
    }

    void RenderModifiers(int companyId)
    {

    }

    void Render()
    {
        var c = SelectedCompany;
        var companyId = c.company.Id;

        CompanyCost.text = RenderCosts(CompanyEconomyUtils.GetCompanyCost(GameContext, companyId));

        RenderBaseCosts(companyId, c);
    }
}

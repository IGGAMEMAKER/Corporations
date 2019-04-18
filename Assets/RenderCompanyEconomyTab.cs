using Assets.Utils;
using UnityEngine.UI;

public class RenderCompanyEconomyTab : View
{
    public Text Income;
    public Text Maintenance;
    public ColoredValuePositiveOrNegative Change;

    public Hint IncomeHint;
    public Hint MaintenanceHint;

    void Render()
    {
        int companyId = GetComponent<SetTargetCompany>().companyId;

        var company = CompanyUtils.GetCompanyById(GameContext, companyId);

        Income.text = "$" + ValueFormatter.Shorten(CompanyEconomyUtils.GetCompanyIncome(company, GameContext));
        Maintenance.text = "$" + ValueFormatter.Shorten(CompanyEconomyUtils.GetCompanyMaintenance(company, GameContext));

        Change.UpdateValue(CompanyEconomyUtils.GetBalanceChange(company, GameContext));

        IncomeHint.SetHint(ProductEconomicsUtils.GetIncomeDescription(company));
        MaintenanceHint.SetHint(ProductEconomicsUtils.GetMaintenanceDescription(company));
    }

    private void Update()
    {
        Render();
    }
}

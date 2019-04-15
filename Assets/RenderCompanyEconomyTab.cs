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

        Income.text = "$" + ValueFormatter.Shorten(ProductEconomicsUtils.GetIncome(company));
        Maintenance.text = "$" + ValueFormatter.Shorten(ProductEconomicsUtils.GetMaintenance(company));

        Change.UpdateValue(ProductEconomicsUtils.GetBalance(company));

        IncomeHint.SetHint(ProductEconomicsUtils.GetIncomeDescription(company));
        MaintenanceHint.SetHint(ProductEconomicsUtils.GetMaintenanceDescription(company));
    }

    private void Update()
    {
        Render();
    }
}

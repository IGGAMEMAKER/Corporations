using Assets.Utils;
using UnityEngine.UI;

public class RenderCompanyEconomyTab : View
{
    public Text Income;
    public Text Maintenance;
    public ColoredValuePositiveOrNegative Change;

    public Hint IncomeHint;
    public Hint MaintenanceHint;

    public override void ViewRender()
    {
        base.ViewRender();

        int companyId = GetComponent<SetTargetCompany>().companyId;

        var company = CompanyUtils.GetCompanyById(GameContext, companyId);

        Income.text = "$" + ValueFormatter.Shorten(CompanyEconomyUtils.GetCompanyIncome(company, GameContext));
        IncomeHint.SetHint(CompanyEconomyUtils.GetIncomeDescription(GameContext, companyId));

        Maintenance.text = "$" + ValueFormatter.Shorten(CompanyEconomyUtils.GetCompanyMaintenance(company, GameContext));
        MaintenanceHint.SetHint(CompanyEconomyUtils.GetMaintenanceDescription(GameContext, companyId));

        Change.UpdateValue(CompanyEconomyUtils.GetBalanceChange(company, GameContext));
    }
}

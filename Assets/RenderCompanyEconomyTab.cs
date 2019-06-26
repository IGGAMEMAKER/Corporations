using Assets.Utils;
using UnityEngine.UI;

public class RenderCompanyEconomyTab : View
{
    public Text Income;
    public Text Maintenance;

    public Hint IncomeHint;
    public Hint MaintenanceHint;

    public override void ViewRender()
    {
        base.ViewRender();

        int companyId = GetComponent<SetTargetCompany>().companyId;

        var company = CompanyUtils.GetCompanyById(GameContext, companyId);

        Income.text = "$" + Format.Shorten(CompanyEconomyUtils.GetCompanyIncome(company, GameContext));
        IncomeHint.SetHint(CompanyEconomyUtils.GetIncomeDescription(GameContext, companyId));

        Maintenance.text = "$" + Format.Shorten(CompanyEconomyUtils.GetCompanyMaintenance(company, GameContext));
        MaintenanceHint.SetHint(CompanyEconomyUtils.GetMaintenanceDescription(GameContext, companyId));
    }
}

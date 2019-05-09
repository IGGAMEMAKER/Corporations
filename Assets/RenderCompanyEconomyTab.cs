using Assets.Utils;
using UnityEngine.UI;

public class RenderCompanyEconomyTab : View
    , IAnyDateListener
{
    public Text Income;
    public Text Maintenance;
    public ColoredValuePositiveOrNegative Change;

    public Hint IncomeHint;
    public Hint MaintenanceHint;

    private void OnEnable()
    {
        Render();

        ListenDateChanges(this);
    }

    void Render()
    {
        int companyId = GetComponent<SetTargetCompany>().companyId;

        var company = CompanyUtils.GetCompanyById(GameContext, companyId);

        Income.text = "$" + ValueFormatter.Shorten(CompanyEconomyUtils.GetCompanyIncome(company, GameContext));
        Maintenance.text = "$" + ValueFormatter.Shorten(CompanyEconomyUtils.GetCompanyMaintenance(company, GameContext));

        Change.UpdateValue(CompanyEconomyUtils.GetBalanceChange(company, GameContext));

        IncomeHint.SetHint(CompanyEconomyUtils.GetIncomeDescription(GameContext, companyId));
        MaintenanceHint.SetHint(CompanyEconomyUtils.GetMaintenanceDescription(GameContext, companyId));
    }

    void IAnyDateListener.OnAnyDate(GameEntity entity, int date)
    {
        Render();
    }
}

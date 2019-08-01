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

        Income.text = "$" + Format.Minify(CompanyEconomyUtils.GetCompanyIncome(company, GameContext));
        IncomeHint.SetHint(GetIncomeDescription(GameContext, companyId));

        Maintenance.text = "$" + Format.Minify(CompanyEconomyUtils.GetCompanyMaintenance(company, GameContext));
        MaintenanceHint.SetHint(GetMaintenanceDescription(GameContext, companyId));
    }


    internal string GetIncomeDescription(GameContext context, int companyId)
    {
        var c = CompanyUtils.GetCompanyById(context, companyId);

        if (CompanyUtils.IsProductCompany(c))
            return GetProductCompanyIncomeDescription(c, context);

        return GetGroupIncomeDescription(context, companyId);
    }


    internal string GetMaintenanceDescription(GameContext context, int companyId)
    {
        var c = CompanyUtils.GetCompanyById(context, companyId);

        if (CompanyUtils.IsProductCompany(c))
            return GetProductCompanyMaintenanceDescription(c, context);

        return GetGroupMaintenanceDescription(context, companyId);
    }


    private string GetProductCompanyIncomeDescription(GameEntity gameEntity, GameContext gameContext)
    {
        var income = CompanyEconomyUtils.GetProductCompanyIncome(gameEntity, gameContext);

        return $"Income of this company equals {Format.Money(income)}";
    }

    internal string GetProductCompanyMaintenanceDescription(GameEntity company, GameContext gameContext)
    {
        var maintenance = CompanyEconomyUtils.GetProductCompanyMaintenance(company, gameContext);

        return $"Maintenance of {company.company.Name} equals {Format.Money(maintenance)}";
    }

    private  string GetGroupMaintenanceDescription(GameContext context, int companyId)
    {
        string description = "Group maintenance:\n";

        var holdings = CompanyUtils.GetCompanyHoldings(context, companyId, false);

        foreach (var h in holdings)
        {
            var c = CompanyUtils.GetCompanyById(context, h.companyId);

            string name = c.company.Name;
            long income = CompanyEconomyUtils.GetCompanyMaintenance(c, context);
            string tiedIncome = Format.Minify(h.control * income / 100);

            description += $"\n  {name}: -${tiedIncome} ({h.control}%)";
        }

        return description;
    }


    private string GetGroupIncomeDescription(GameContext context, int companyId)
    {
        string description = "Group income:\n";

        var holdings = CompanyUtils.GetCompanyHoldings(context, companyId, false);

        foreach (var h in holdings)
        {
            var c = CompanyUtils.GetCompanyById(context, h.companyId);

            string name = c.company.Name;
            long income = CompanyEconomyUtils.GetCompanyIncome(c, context);
            string tiedIncome = Format.Minify(h.control * income / 100);

            description += $"\n  {name}: +${tiedIncome} ({h.control}%)";
        }

        return description;
    }
}

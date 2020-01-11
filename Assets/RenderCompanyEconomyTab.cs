using Assets.Core;
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

        var company = Companies.GetCompany(GameContext, companyId);

        Income.text = "$" + Format.Minify(Economy.GetCompanyIncome(GameContext, company));
        IncomeHint.SetHint(GetIncomeDescription(GameContext, companyId));

        Maintenance.text = "$" + Format.Minify(Economy.GetCompanyMaintenance(GameContext, company));
        MaintenanceHint.SetHint(GetMaintenanceDescription(GameContext, companyId));
    }


    internal string GetIncomeDescription(GameContext context, int companyId)
    {
        var c = Companies.GetCompany(context, companyId);

        if (Companies.IsProductCompany(c))
            return GetProductCompanyIncomeDescription(c, context);

        return GetGroupIncomeDescription(context, companyId);
    }


    internal string GetMaintenanceDescription(GameContext context, int companyId)
    {
        var c = Companies.GetCompany(context, companyId);

        if (Companies.IsProductCompany(c))
            return GetProductCompanyMaintenanceDescription(c, context);

        return GetGroupMaintenanceDescription(context, companyId);
    }


    private string GetProductCompanyIncomeDescription(GameEntity gameEntity, GameContext gameContext)
    {
        var income = Economy.GetProductCompanyIncome(gameEntity, gameContext);

        return $"Income of this company equals {Format.Money(income)}";
    }

    internal string GetProductCompanyMaintenanceDescription(GameEntity company, GameContext gameContext)
    {
        var maintenance = Economy.GetProductCompanyMaintenance(company, gameContext);

        return $"Maintenance of {company.company.Name} equals {Format.Money(maintenance)}";
    }

    private  string GetGroupMaintenanceDescription(GameContext context, int companyId)
    {
        string description = "Group maintenance:\n";

        var holdings = Companies.GetCompanyHoldings(context, companyId, false);

        foreach (var h in holdings)
        {
            var c = Companies.GetCompany(context, h.companyId);

            string name = c.company.Name;
            long income = Economy.GetCompanyMaintenance(context, c);
            string tiedIncome = Format.Minify(h.control * income / 100);

            description += $"\n  {name}: -${tiedIncome} ({h.control}%)";
        }

        return description;
    }


    private string GetGroupIncomeDescription(GameContext context, int companyId)
    {
        string description = "Group income:\n";

        var holdings = Companies.GetCompanyHoldings(context, companyId, false);

        foreach (var h in holdings)
        {
            var c = Companies.GetCompany(context, h.companyId);

            string name = c.company.Name;
            long income = Economy.GetCompanyIncome(context, c);
            string tiedIncome = Format.Minify(h.control * income / 100);

            description += $"\n  {name}: +${tiedIncome} ({h.control}%)";
        }

        return description;
    }
}

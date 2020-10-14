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

        var company = SelectedCompany;

        Income.text = "$" + Format.Minify(Economy.GetCompanyIncome(Q, company));
        IncomeHint.SetHint(GetIncomeDescription(Q, company));

        Maintenance.text = "$" + Format.Minify(Economy.GetCompanyMaintenance(Q, company));
        MaintenanceHint.SetHint(GetMaintenanceDescription(Q, company));
    }


    internal string GetIncomeDescription(GameContext context, GameEntity c)
    {
        if (Companies.IsProductCompany(c))
            return GetProductCompanyIncomeDescription(c, context);

        return GetGroupIncomeDescription(context, c);
    }


    internal string GetMaintenanceDescription(GameContext context, GameEntity company)
    {
        if (Companies.IsProductCompany(company))
            return GetProductCompanyMaintenanceDescription(company, context);

        return GetGroupMaintenanceDescription(context, company);
    }


    private string GetProductCompanyIncomeDescription(GameEntity gameEntity, GameContext gameContext)
    {
        var income = Economy.GetIncomeFromProduct(gameEntity);

        return $"Income of this company equals {Format.Money(income)}";
    }

    internal string GetProductCompanyMaintenanceDescription(GameEntity company, GameContext gameContext)
    {
        var maintenance = Economy.GetProductCompanyMaintenance(company, gameContext);

        return $"Maintenance of {company.company.Name} equals {Format.Money(maintenance)}";
    }

    private string GetGroupMaintenanceDescription(GameContext context, GameEntity company)
    {
        string description = "Group maintenance:\n";

        var holdings = Investments.GetHoldings(context, company, false);

        foreach (var h in holdings)
        {
            var c = Companies.Get(context, h.companyId);

            string name = c.company.Name;

            long income = Economy.GetCompanyMaintenance(context, c);
            string tiedIncome = Format.Minify(h.control * income / 100);

            description += $"\n  {name}: -${tiedIncome} ({h.control}%)";
        }

        return description;
    }


    private string GetGroupIncomeDescription(GameContext context, GameEntity company)
    {
        string description = "Group income:\n";

        var holdings = Investments.GetHoldings(context, company, false);

        foreach (var h in holdings)
        {
            var c = Companies.Get(context, h.companyId);

            string name = c.company.Name;

            long income = Economy.GetCompanyIncome(context, c);
            string tiedIncome = Format.Minify(h.control * income / 100);

            description += $"\n  {name}: +${tiedIncome} ({h.control}%)";
        }

        return description;
    }
}

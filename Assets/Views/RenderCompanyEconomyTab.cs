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

        Income.text = "$" + Format.Minify(Economy.GetIncome(Q, company));
        IncomeHint.SetHint(GetIncomeDescription(Q, company));

        Maintenance.text = "$" + Format.Minify(Economy.GetMaintenance(Q, company));
        MaintenanceHint.SetHint(GetMaintenanceDescription(Q, company));
    }


    internal string GetIncomeDescription(GameContext context, GameEntity company)
    {
        if (company.hasProduct)
            return GetProductCompanyIncomeDescription(company, context);

        return GetGroupIncomeDescription(context, company);
    }


    internal string GetMaintenanceDescription(GameContext context, GameEntity company)
    {
        if (company.hasProduct)
            return GetProductCompanyMaintenanceDescription(company, context);

        return GetGroupMaintenanceDescription(context, company);
    }


    private string GetProductCompanyIncomeDescription(GameEntity gameEntity, GameContext gameContext)
    {
        var income = Economy.GetProductIncome(gameEntity);

        return $"Income of this company equals {Format.Money(income)}";
    }

    internal string GetProductCompanyMaintenanceDescription(GameEntity company, GameContext gameContext)
    {
        var maintenance = Economy.GetProductMaintenance(company, gameContext);

        return $"Maintenance of {company.company.Name} equals {Format.Money(maintenance)}";
    }

    private string GetGroupMaintenanceDescription(GameContext context, GameEntity company)
    {
        string description = "Group maintenance:\n";

        var holdings = Investments.GetHoldings(company, context, false);

        foreach (var h in holdings)
        {
            var c = h.company;

            string name = c.company.Name;

            long income = Economy.GetMaintenance(context, c);
            string tiedIncome = Format.Minify(h.control * income / 100);

            description += $"\n  {name}: -${tiedIncome} ({h.control}%)";
        }

        return description;
    }


    private string GetGroupIncomeDescription(GameContext context, GameEntity company)
    {
        string description = "Group income:\n";

        var holdings = Investments.GetHoldings(company, context, false);

        foreach (var h in holdings)
        {
            var c = h.company;

            string name = c.company.Name;

            long income = Economy.GetIncome(context, c);
            string tiedIncome = Format.Minify(h.control * income / 100);

            description += $"\n  {name}: +${tiedIncome} ({h.control}%)";
        }

        return description;
    }
}

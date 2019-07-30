using Assets.Utils;

public class RenderPersonalCapital : UpgradedParameterView
{
    public override string RenderHint()
    {
        return "";
    }

    GameEntity human
    {
        get
        {
            return SelectedHuman;
        }
    }

    public override string RenderValue()
    {
        var text = "";

        if (human.hasCompanyResource)
        {
            text += "Holdings cost: " + Format.Money(InvestmentUtils.GetInvestorCapitalCost(GameContext, SelectedHuman));

            //if (SelectedHuman == Me)
            //    text += "\nCash: " + Format.Money(human.companyResource.Resources.money);
        } else
        {
            var role = human.worker.WorkerRole;
            //text = "Salary: $" + TeamUtils.GetSalary(role);
            text = "???";
        }

        return text;
    }
}

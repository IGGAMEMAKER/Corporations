using Assets.Core;

public class RenderPersonalCapital : UpgradedParameterView
{
    public override string RenderHint() => "";

    public override string RenderValue()
    {
        var text = "";

        var human = SelectedHuman;

        if (human.hasCompanyResource)
        {
            text += "Holdings cost: " + Format.Money(Investments.GetInvestorCapitalCost(Q, human));

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

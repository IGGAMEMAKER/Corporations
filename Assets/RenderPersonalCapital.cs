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
        if (human.hasCompanyResource)
        {
            return Format.Money(human.companyResource.Resources.money);
        } else
        {
            var role = human.worker.WorkerRole;

            return "Salary: $" + TeamUtils.GetSalary(role);
        }
    }
}

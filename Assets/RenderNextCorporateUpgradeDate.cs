using Assets.Core;

public class RenderNextCorporateUpgradeDate : ParameterView
{
    public override string RenderValue()
    {
        //return "RenderNextCorporateUpgradeValue";
        var task = Cooldowns.GetTask(Q, new CompanyTaskUpgradeCulture(MyCompany.company.Id));
        //Cooldowns.Get(Q, new CooldownUpgradeCorporateCulture(MyCompany.company.Id), out var cooldown);

        if (task == null)
            return "You can change corporate culture!";

        var days = task.EndTime - CurrentIntDate;

        return $"Next culture change in {days} days";
    }
}

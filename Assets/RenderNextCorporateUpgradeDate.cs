using Assets.Core;

public class RenderNextCorporateUpgradeDate : ParameterView
{
    public override string RenderValue()
    {
        return "RenderNextCorporateUpgradeValue";
        //Cooldowns.TryGetCooldown(Q, new CooldownUpgradeCorporateCulture(MyCompany.company.Id), out var cooldown);

        //if (cooldown == null)
        //    return "You can change corporate culture!";

        //var days = cooldown.EndDate - CurrentIntDate;

        //return $"You will be able to change corporate culture in {days} days";
    }
}

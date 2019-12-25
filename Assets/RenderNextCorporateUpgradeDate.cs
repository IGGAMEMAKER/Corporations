using Assets.Utils;

public class RenderNextCorporateUpgradeDate : UpgradedParameterView
{
    public override string RenderHint() => "";

    public override string RenderValue()
    {
        CooldownUtils.TryGetCooldown(GameContext, new CooldownUpgradeCorporateCulture(MyCompany.company.Id), out var cooldown);

        if (cooldown == null)
            return "You can change corporate culture!";

        var days = cooldown.EndDate - CurrentIntDate;

        return $"You will be able to change corporate culture in {days} days";
    }
}

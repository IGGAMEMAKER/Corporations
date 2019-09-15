using Assets.Utils;

public class RenderConceptProgress : UpgradedParameterView
{
    private int CompanyId;
    int days = 0;

    public void SetCompanyId(int companyId)
    {
        CompanyId = companyId;
    }

    public override string RenderHint()
    {
        return $"Will upgrade concept in {days} days";
    }

    public override string RenderValue()
    {
        Cooldown c;
        CooldownUtils.TryGetCooldown(GameContext, new CooldownImproveConcept(CompanyId), out c);

        if (c == null)
            return "";

        days = c.EndDate - CurrentIntDate;

        return $"{days}d";
        //return $"Improving {days}d";
        //return $"Upgrading app\n{days} days";
    }
}

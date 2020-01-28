using Assets.Core;

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
        if (!Companies.IsExploredCompany(GameContext, CompanyId))
            return "";

        var company = Companies.Get(GameContext, CompanyId);
        var willInnovate = Products.IsWillInnovate(company, GameContext);
        var innovationChance = Products.GetInnovationChance(company, GameContext);

        var description = $"Working on product upgrade ({days} days)\n\n";

        if (willInnovate)
            description += "Has " + innovationChance + "% chance to make an innovation";
        else
            description += "Will upgrade product guaranteedly, cause they are behind market";

        return description;
    }

    public override string RenderValue()
    {
        Cooldowns.TryGetCooldown(GameContext, new CooldownImproveConcept(CompanyId), out Cooldown c);

        if (c == null)
            return "";

        days = c.EndDate - CurrentIntDate;

        if (!Companies.IsExploredCompany(GameContext, CompanyId))
            return "";

        return $"{days}d";
        //return $"Improving {days}d";
        //return $"Upgrading app\n{days} days";
    }
}

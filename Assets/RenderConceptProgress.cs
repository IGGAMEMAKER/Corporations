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
        if (!Companies.IsExploredCompany(Q, CompanyId))
            return "";

        var company = Companies.Get(Q, CompanyId);
        var willInnovate = Products.IsWillInnovate(company, Q);
        var innovationChance = Products.GetInnovationChance(company, Q);

        var description = $"Working on product upgrade ({days} days)\n\n";

        if (willInnovate)
            description += "Has " + innovationChance + "% chance to make an innovation";
        else
            description += "Will upgrade product guaranteedly, cause they are behind market";

        return description;
    }

    public override string RenderValue()
    {
        Cooldowns.TryGetCooldown(Q, new CooldownImproveConcept(CompanyId), out Cooldown c);

        if (c == null)
            return "";

        days = c.EndDate - CurrentIntDate;

        if (!Companies.IsExploredCompany(Q, CompanyId))
            return "";

        return $"{days}d";
        //return $"Improving {days}d";
        //return $"Upgrading app\n{days} days";
    }
}

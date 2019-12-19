using Assets.Utils;

public class RenderOpinionAboutUs : UpgradedParameterView
{
    public override string RenderHint()
    {
        if (Companies.IsCompanyRelatedToPlayer(GameContext, SelectedCompany))
            return "";

        if (Companies.IsFinancialStructure(SelectedCompany))
            return "";

        return Companies.GetPartnerability(MyCompany, SelectedCompany, GameContext).ToString();
    }

    public override string RenderValue()
    {
        if (Companies.IsCompanyRelatedToPlayer(GameContext, SelectedCompany))
            return "---";

        if (Companies.IsFinancialStructure(SelectedCompany))
            return "??";

        var opinion = Companies.GetPartnershipOpinionAboutUs(MyCompany, SelectedCompany, GameContext);

        return Visuals.PositiveOrNegativeMinified(opinion);
    }
}

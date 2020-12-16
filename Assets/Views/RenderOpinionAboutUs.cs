using Assets.Core;

public class RenderOpinionAboutUs : UpgradedParameterView
{
    public override string RenderHint()
    {
        if (Companies.IsDirectlyRelatedToPlayer(Q, SelectedCompany))
            return "";

        if (Companies.IsFinancialStructure(SelectedCompany))
            return "";

        return Companies.GetPartnerability(MyCompany, SelectedCompany, Q).ToString();
    }

    public override string RenderValue()
    {
        if (Companies.IsDirectlyRelatedToPlayer(Q, SelectedCompany))
            return "---";

        if (Companies.IsFinancialStructure(SelectedCompany))
            return "??";

        var opinion = Companies.GetPartnershipOpinionAboutUs(MyCompany, SelectedCompany, Q);

        return Visuals.PositiveOrNegativeMinified(opinion);
    }
}

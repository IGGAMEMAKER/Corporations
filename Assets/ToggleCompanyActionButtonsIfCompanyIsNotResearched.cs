using Assets.Utils;

public class ToggleCompanyActionButtonsIfCompanyIsNotResearched : ToggleOnSomeCondition
{
    public override bool Condition()
    {
        // SelectedCompany != null && (
        return SelectedCompany.hasResearch || CompanyUtils.IsCompanyRelatedToPlayer(GameContext, SelectedCompany);
    }
}

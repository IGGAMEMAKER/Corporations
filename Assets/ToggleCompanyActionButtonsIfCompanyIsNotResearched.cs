using Assets.Utils;

public class ToggleCompanyActionButtonsIfCompanyIsNotResearched : ToggleOnSomeCondition
{
    public override bool Condition()
    {
        var hasTask = CooldownUtils.IsHasTask(GameContext, new CompanyTaskExploreCompany(SelectedCompany.company.Id));

        return SelectedCompany.hasResearch || hasTask || CompanyUtils.IsCompanyRelatedToPlayer(GameContext, SelectedCompany);
    }
}
